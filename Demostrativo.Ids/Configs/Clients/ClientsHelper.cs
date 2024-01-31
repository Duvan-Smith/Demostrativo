using Demostrativo.Ids.Configs.App;
using IdentityServer4.Models;

namespace Demostrativo.Ids.Configs.Clients;

public static class ClientsHelper
{
    public static IEnumerable<Client> GetClients()
    {
        var hojaDeVidaApp = new HojaDeVidaApp();

        return new List<Client>
        {
            hojaDeVidaApp.GetApiClient(),
            hojaDeVidaApp.GetWebClient(),
        };
    }
}