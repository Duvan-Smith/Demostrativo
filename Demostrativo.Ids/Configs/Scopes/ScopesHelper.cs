using Demostrativo.Ids.Configs.App;
using IdentityServer4.Models;

namespace Demostrativo.Ids.Configs.Scopes;

public static class ScopesHelper
{
    public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new HojaDeVidaApp().GetApiScope(),
        };
}