using Demostrativo.Jwt.Persistence.Ejemploes;
using Microsoft.Extensions.DependencyInjection;

namespace Demostrativo.Jwt.Aplication.Core.Ejemploes;

public static class EjemploServiceConfigurator
{
    public static void ConfiguratorEjemploService(this IServiceCollection services)
    {
        services.ConfigurationEjemploRepository();
    }
}