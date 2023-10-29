using Demostrativo.Jwt.Aplication.Core;
using Demostrativo.Jwt.Persistence.Context;
using Micro.Infra.Bus;
using Micro.Infra.IoC;
using Microsoft.EntityFrameworkCore;

const string CorsPolicyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy(CorsPolicyName, builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(new[]
        {
            "http://localhost:5005",
        })
        .SetIsOriginAllowedToAllowWildcardSubdomains();
}));

var DB_HOST = builder.Configuration.GetSection("DB_HOST").Get<string>();
var DB_PORT = builder.Configuration.GetSection("DB_PORT").Get<string>();
var DB_NAME_PD = builder.Configuration.GetSection("DB_NAME_PD").Get<string>();
var DB_USER = builder.Configuration.GetSection("DB_USER").Get<string>();
var DB_PASSWORD = builder.Configuration.GetSection("DB_PASSWORD").Get<string>();

if (
    string.IsNullOrEmpty(DB_HOST) ||
    string.IsNullOrEmpty(DB_PORT) ||
    string.IsNullOrEmpty(DB_NAME_PD) ||
    string.IsNullOrEmpty(DB_USER) ||
    string.IsNullOrEmpty(DB_PASSWORD)
    )
{
    builder.Services
            .ConfigurationAplicationService(
                builder.Configuration.GetConnectionString("JwtDB") ?? "");
}
else
{
    builder.Services
        .ConfigurationAplicationService(
            $"Server={DB_HOST};Database={DB_NAME_PD};Port={DB_PORT};User Id={DB_USER};Password={DB_PASSWORD};");
}

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.ConfigurationAplicationRabbitMQService();

//builder.Services.TryAddTransient<IEventHandler<CreateEjemplosEvent>, EjemplosEventHandler>();
//builder.Services.TryAddTransient<EjemplosEventHandler>();

//builder.Services.TryAddTransient<IEventHandler<DeleteEjemplosEvent>, EjemplosDeleteEventHandler>();
//builder.Services.TryAddTransient<EjemplosDeleteEventHandler>();

var app = builder.Build();

//var eventBus = app.Services.GetRequiredService<IEventBus>();
//eventBus.Subscribe<CreateEjemplosEvent, EjemplosEventHandler>();

//var eventBusD = app.Services.GetRequiredService<IEventBus>();
//eventBusD.Subscribe<DeleteEjemplosEvent, EjemplosDeleteEventHandler>();
//builder.Services.ConfiguratorDeleteEjemplosFD(builder);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var supportedCultures = new string[] { "es-ES" };
app.UseRequestLocalization(supportedCultures);
app.UseCors(CorsPolicyName);

app.UseAuthorization();

app.MapControllers();

using var services = app.Services.CreateScope();
var dbContext = services.ServiceProvider.GetService<PersistenceDbContext>();
try
{
    dbContext?.Database.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

app.Run();