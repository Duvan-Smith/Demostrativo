using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Demostrativo.Ids.Configs.App;

public abstract class ConfigBase
{
    public ConfigBase() => AllowedScopes.Add(ResourceName);

    protected abstract List<string> ListScopes { get; }

    public string Secret = "secretProCodeGuide";

    protected abstract string AppIdentifier { get; }
    protected abstract List<string> AppExternalIdentifier { get; }
    public abstract string AppName { get; }
    public abstract string Description { get; }
    public abstract string DisplayName { get; }
    public abstract string FrontendPort { get; }
    public abstract string BackendPort { get; }
    public abstract string GatewayPort { get; }
    public virtual string BackendUrl => $"demostrativo.{AppIdentifier}-ws.azurewebsites.net";
    public virtual string FrontendUrl => $"demostrativo.{AppIdentifier}-web.azurewebsites.net";
    public virtual ICollection<Secret> Secrets => new List<Secret> { new Secret(Secret.ToSha256()) };

    protected virtual List<string> AllowedScopes { get; } = new List<string>
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,
        "role"
    };

    public virtual IEnumerable<ApiScope> RequiredScopes { get; } = new List<ApiScope>();
    protected virtual IEnumerable<Client> AllowedClients { get; } = new List<Client>();

    public virtual string ResourceName => $"demostrativo.{AppIdentifier}.api";

    public ICollection<string> GetUserClaims() =>
        new string[]
        {
            "role",
        };

    public ApiResource GetApiResource() => new(ResourceName)
    {
        DisplayName = DisplayName,
        Description = Description,
        Scopes = AllowedScopes,
        ApiSecrets = Secrets,
        UserClaims = GetUserClaims(),
    };

    public ApiScope GetApiScope() => new(ResourceName, Description);

    public Client GetApiClient() => new()
    {
        ClientId = $"demostrativo.{AppIdentifier}.api.client",
        ClientName = $"{AppName} Web Api",
        ClientSecrets = Secrets,
        AllowedGrantTypes = GrantTypes.ClientCredentials,

        AllowedCorsOrigins = new List<string> {
            $"https://localhost:{FrontendPort}",
            $"https://localhost:{BackendPort}",
            $"https://localhost:{GatewayPort}",

            $"http://localhost:{FrontendPort}",
            $"http://localhost:{BackendPort}",
            $"http://localhost:{GatewayPort}",

            $"https://{FrontendUrl}",
            $"https://{BackendUrl}",
        },

        RedirectUris = new List<string>()
        {
            "com.efactory.sintratplu://oauth2redirect",

            $"https://localhost:{BackendPort}/index.html",
            $"https://localhost:{BackendPort}/oauth2-redirect.html",

            $"https://{FrontendUrl}",
            $"https://{BackendUrl}",

            $"https://{FrontendUrl}/index.html",
            $"https://{BackendUrl}/index.html",

            $"https://{FrontendUrl}/oauth2-redirect.html",
            $"https://{BackendUrl}/oauth2-redirect.html",
        },
        AllowedScopes = AllowedScopes,
    };

    public Client GetWebClient() => new()
    {
        ClientId = $"demostrativo.{AppIdentifier}.web.client",
        ClientName = $"demostrativo.{AppIdentifier}.web.client",
        AllowedGrantTypes = GrantTypes.Code,
        AllowAccessTokensViaBrowser = true,
        RequirePkce = true,
        AllowPlainTextPkce = false,
        AllowOfflineAccess = true,
        AccessTokenType = AccessTokenType.Jwt,
        RequireClientSecret = false,

        AllowedCorsOrigins = new List<string>
        {
            "https://oauth.pstmn.io",

            $"https://localhost:{FrontendPort}",
            $"https://localhost:{BackendPort}",
            $"https://localhost:{GatewayPort}",

            $"http://localhost:{FrontendPort}",
            $"http://localhost:{BackendPort}",
            $"http://localhost:{GatewayPort}",

            $"https://{FrontendUrl}",
            $"https://{BackendUrl}",

            $"http://{FrontendUrl}",
            $"http://{BackendUrl}"
        },

        RedirectUris = new List<string>
        {
            "https://oauth.pstmn.io/v1/callback",

            $"https://localhost:{FrontendPort}/signin-oidc",
            $"https://localhost:{FrontendPort}/login-oidc-callback",
            $"https://localhost:{FrontendPort}/authentication/login-callback",

            $"http://localhost:{FrontendPort}/signin-oidc",
            $"http://localhost:{FrontendPort}/login-oidc-callback",
            $"http://localhost:{FrontendPort}/authentication/login-callback",

            $"{FrontendUrl}/signin-oidc",
            $"{FrontendUrl}/login-oidc-callback",
            $"{FrontendUrl}/authentication/login-callback",

            $"https://{FrontendUrl}/signin-oidc",
            $"https://{FrontendUrl}/authentication/login-callback",
            $"https://{FrontendUrl}/login-oidc-callback",

            $"http://{FrontendUrl}/signin-oidc",
            $"http://{FrontendUrl}/authentication/login-callback",
            $"http://{FrontendUrl}/login-oidc-callback",

            "com.efactory.sintratplu://oauth2redirect",

            $"http://localhost:{FrontendPort}/",
            $"http://localhost:{FrontendPort}",

            $"https://localhost:{FrontendPort}/",
            $"https://localhost:{FrontendPort}",
        },

        //FrontChannelLogoutUri = $"https://localhost:{FrontendPort}/signout-oidc",
        //BackChannelLogoutUri = $"https://localhost:{FrontendPort}/",

        PostLogoutRedirectUris = new List<string>
        {
            $"https://localhost:{FrontendPort}/authentication/logout-callback",
            $"http://localhost:{FrontendPort}/authentication/logout-callback",

            $"https://localhost:{FrontendPort}/signout-oidc",
            $"http://localhost:{FrontendPort}/signout-oidc",

            $"https://localhost:{FrontendPort}/",
            $"http://localhost:{FrontendPort}/",

            $"https://{FrontendUrl}/logout-callback",
            $"http://{FrontendUrl}/logout-callback",

            $"https://{FrontendUrl}/signout-oidc",
            $"http://{FrontendUrl}/signout-oidc",

            $"https://{FrontendUrl}/",
            $"http://{FrontendUrl}/",

            $"{FrontendUrl}/authentication/logout-callback",
            $"{FrontendUrl}/logout-callback",
            $"{FrontendUrl}/signout-oidc",
            $"{FrontendUrl}/"
        },
        AllowedScopes = GenerateScopesGetWebClient()
    };

    public ICollection<string> GenerateScopesGetWebClient()
    {
        var listTemp = AllowedScopes;

        if (AppExternalIdentifier.Count != 0)
            listTemp.AddRange(AppExternalIdentifier);

        return listTemp;
    }
}