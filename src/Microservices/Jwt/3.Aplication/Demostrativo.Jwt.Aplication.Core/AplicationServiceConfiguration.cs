using Demostrativo.Jwt.Aplication.Core.Base.Mapper.Configuration;
using Demostrativo.Jwt.Aplication.Core.Ejemploes;
using Demostrativo.Jwt.PhotoDetection.Persistence.EntityTest.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Demostrativo.Jwt.Aplication.Core;

public static class AplicationServiceConfiguration
{
    public static void ConfigurationAplicationService(this IServiceCollection services, string connectionString)
    {
        services.ConfigureMapper();
        services.ConfigurationPersistenceDbContext(connectionString);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.ConfiguratorEjemploService();
    }
}