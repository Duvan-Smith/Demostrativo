using Demostrativo.Jwt.Cross.Http.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Demostrativo.Jwt.Cross.Http.Configuration;

public static class HttpClientConfigurator
{
    public static void ConfigureHttpClient(this IServiceCollection services, HttpClientOptions parameters, string snapshotKey)
    {
        services.Configure<HttpClientOptions>(snapshotKey, o => o.CopyFrom(parameters));

        services.AddHttpClient<IGenericHttpClient, GenericHttpClient>("Demostrativo.Jwt.MobileAgents.Web.ServerAPI")
            .AddHttpMessageHandler<AccessTokenMessageHandler>();

        services.AddTransient<AccessTokenMessageHandler>();
        services.AddTransient(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("Demostrativo.Jwt.MobileAgents.Web.ServerAPI"));
    }

    public static void ConfigureHttpClients(this IServiceCollection services, bool isDevelopment = false)
    {
        var port = 443;
        string hostname;
        const string protocol = "https";

        switch (isDevelopment)
        {
            case false:
                hostname = "site-pi-core-ws.azurewebsites.net";
                break;

            case true:
                hostname = "localhost";
                port = 5006;
                break;
        }

        //services.ConfigureEntityTestHttpClient(new HttpClientOptions
        //{
        //    Context = "test",
        //    Hostname = hostname,
        //    Port = port,
        //    Controller = "EnityTest",
        //    ServiceProtocol = protocol
        //});
    }
}