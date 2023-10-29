using MediatR;
using Micro.Domain.Core.Bus;
using Micro.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Micro.Infra.IoC;

public static class DependencyContainer
{
    public static void ConfigurationAplicationRabbitMQService(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
        {
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            var optionsFactory = sp.GetRequiredService<IOptions<RabbitMQSettings>>();
            return new RabbitMQBus(sp.GetRequiredService<IMediator>(), sp.GetRequiredService<ILogger<RabbitMQBus>>(), optionsFactory, scopeFactory);
        });
    }
}