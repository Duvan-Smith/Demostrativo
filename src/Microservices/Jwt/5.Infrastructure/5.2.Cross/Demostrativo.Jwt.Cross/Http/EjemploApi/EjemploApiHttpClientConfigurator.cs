using Demostrativo.Jwt.Cross.Http.Configuration;
using Demostrativo.Jwt.Cross.Http.EjemploApi;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection.Extensions;

public static class EjemploApiHttpClientConfigurator
{
    public static void ConfigureEjemploApiHttpClient(this IServiceCollection services, HttpClientOptions parameters)
    {
        services.ConfigureHttpClient(parameters, nameof(EjemploApiHttpClient));
        services.TryAddTransient<IEjemploApiHttpClient, EjemploApiHttpClient>();
    }
}