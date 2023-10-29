﻿using MediatR;
using Micro.Domain.Core.Bus;
using Micro.Domain.Core.Commands;
using Micro.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Micro.Infra.Bus;

public class RabbitMQBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly List<Type> _eventTypes;
    private readonly ILogger<RabbitMQBus> _logger;
    private readonly RabbitMQSettings _rabbitMQSettings;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMQBus(IMediator mediator, ILogger<RabbitMQBus> logger, IOptions<RabbitMQSettings> rabbitMQSettings, IServiceScopeFactory serviceScopeFactory)
    {
        _mediator = mediator;
        _eventTypes = new List<Type>();
        _logger = logger;
        _rabbitMQSettings = rabbitMQSettings.Value;
        _handlers = new Dictionary<string, List<Type>>();
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Publish<T>(T @event) where T : Event
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.Hostname,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", eventName, null, body);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: ex.Message);
            if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                _logger.LogError(ex, message: ex.InnerException.Message);
        }
    }

    public Task SendCommand<T>(T command) where T : Command
    {
        try
        {
            return _mediator.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: ex.Message);
            if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                _logger.LogError(ex, message: ex.InnerException.Message);
            return Task.CompletedTask;
        }
    }

    public void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        try
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"El handler exception {handlerType.Name} ya fue registrado anteriormente por '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: ex.Message);
            if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                _logger.LogError(ex, message: ex.InnerException.Message);
        }
    }

    private void StartBasicConsume<T>() where T : Event
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMQSettings.Hostname,
            UserName = _rabbitMQSettings.Username,
            Password = _rabbitMQSettings.Password,
            DispatchConsumersAsync = true
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var eventName = typeof(T).Name;

        channel.QueueDeclare(eventName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += Consumer_Received;

        channel.BasicConsume(eventName, true, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.Span);

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.ContainsKey(eventName))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];

                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);  //Activator.CreateInstance(subscription);
                    if (handler == null) continue;
                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}