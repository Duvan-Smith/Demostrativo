namespace Demostrativo.Jwt.Cross.Http.Base;

internal abstract class MicroserviceHttpClientBase : IMicroserviceHttpClientBase
{
    protected IGenericHttpClient HttpClient { get; }

    public MicroserviceHttpClientBase(IGenericHttpClient httpClient) =>
        HttpClient = httpClient;

    public async Task<bool> CheckHealt() =>
        await HttpClient.GetAsync<bool>(nameof(CheckHealt), CancellationToken.None).ConfigureAwait(false);
}