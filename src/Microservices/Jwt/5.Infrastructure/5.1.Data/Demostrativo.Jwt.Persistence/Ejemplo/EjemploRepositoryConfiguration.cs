using Demostrativo.Jwt.Domain.Ejemplo;
using Microsoft.Extensions.DependencyInjection;

namespace Demostrativo.Jwt.Persistence.Ejemploes;

public static class EjemploRepositoryConfiguration
{
    public static void ConfigurationEjemploRepository(this IServiceCollection services) =>
        services.AddScoped<IEjemploRepository, EjemploRepository>();
}