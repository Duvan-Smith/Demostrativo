namespace Demostrativo.Jwt.Cross.Http.Configuration;

public class AccessTokenMessageHandler : DelegatingHandler
{
    public AccessTokenMessageHandler()
    { }

    public string[] AuthorizedUris { get; set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //if (GetBaseUriOrAuthorizedUris().Any(u => u.IsBaseOf(request.RequestUri)))
        //{
        //    var accessToken = await (AuthenticationStateProvider as OidcAuthenticationStateProvider).GetAccessToken();
        //    if (!accessToken.IsNullOrEmpty())
        //        request.Headers.Authorization = new AuthenticationHeaderValue(IdentityConstants.TokenTypes.Bearer, accessToken);
        //}
        return await base.SendAsync(request, cancellationToken);
    }
}