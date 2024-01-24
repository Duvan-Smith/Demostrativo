using IdentityServer4;
using IdentityServer4.Models;

namespace ProyectoIds4.IdentityServer4AspNet;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource
            {
                Name = "weatherApi",
                DisplayName = "Weather Api",
                Description = "Allow the application to access Weather Api on your behalf",
                Scopes = new List<string> { "weatherApi.read", "weatherApi.write"},
                ApiSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                UserClaims = new List<string> {"role"},
                //AllowedAccessTokenSigningAlgorithms = { "https://localhost:7167" }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("weatherApi.read", "Read Access to Weather API"),
            new ApiScope("weatherApi.write", "Write Access to Weather API"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },

            //Api Client
            new Client
            {
                ClientId = "weatherApi",
                ClientName = "ASP.NET Core Weather Api",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256()),new Secret("weatherApiProCodeGuide123456789987654321") },
                AllowedScopes = new List<string> {"weatherApi.read"}
            },

            //BS Client
            new Client
            {
                ClientId = "oidcMVCApp",
                ClientName = "Sample ASP.NET Core MVC Web App",
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = true,
                RequirePkce = true,
                AllowPlainTextPkce = false,
                AllowOfflineAccess=true,

                AllowedCorsOrigins =
                {
                    "https://oauth.pstmn.io",
                    "https://localhost:5005",
                },

                RedirectUris = {
                    "https://oauth.pstmn.io/v1/callback",
                    "https://localhost:5005/",
                    "https://localhost:5005/signin-callback-oidc",
                },

                BackChannelLogoutUri = "https://localhost:5005/",

                PostLogoutRedirectUris = {
                    "https://localhost:5005/",
                },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "weatherApi.read"
                },
            },

            //RJ Client
            new Client
            {
                ClientId = "oidcMVCAppR",
                ClientName = "Sample ASP.NET Core MVC Web App",
                //ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = true,
                RequirePkce = true,
                AllowPlainTextPkce = false,
                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,
                RequireClientSecret = false,

                //ClientUri = "https://localhost:3000",

                AllowedCorsOrigins =
                {
                    "https://oauth.pstmn.io",
                    "https://localhost:3000",
                },

                RedirectUris = {
                    "https://oauth.pstmn.io/v1/callback",
                    "https://localhost:3000/signin-oidc",
                    "https://localhost:3000/login-oidc-callback",
                    "https://localhost:3000",
                },

                FrontChannelLogoutUri = "https://localhost:3000/signout-oidc",
                BackChannelLogoutUri = "https://localhost:3000/",

                PostLogoutRedirectUris = {
                    "https://localhost:3000/signout-oidc",
                    "https://localhost:3000/logout-callback",
                },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "weatherApi.read"
                },
            },

            new Client
            {
                ClientId = "oidcMVCApp7193",
                ClientName = "Sample ASP.NET Core MVC Web App",
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { "https://localhost:7193/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7193/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7193/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "weatherApi.read"
                },
                RequirePkce = true,
                AllowPlainTextPkce = false
            }
        };
}