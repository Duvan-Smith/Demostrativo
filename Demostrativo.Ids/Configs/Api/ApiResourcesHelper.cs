using Demostrativo.Ids.Configs.App;
using IdentityServer4.Models;

namespace Demostrativo.Ids.Configs.Api;

public static class ApiResourcesHelper
{
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new HojaDeVidaApp().GetApiResource(),
        };
}