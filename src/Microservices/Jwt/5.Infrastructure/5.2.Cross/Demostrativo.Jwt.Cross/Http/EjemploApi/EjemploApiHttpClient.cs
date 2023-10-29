using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Cross.Http.Base;
using Demostrativo.Jwt.Cross.Http.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Demostrativo.Jwt.Cross.Http.EjemploApi;

internal class EjemploApiHttpClient : MicroserviceHttpClientBase, IEjemploApiHttpClient
{
    private readonly HttpClientOptions _httpClientOptions;

    public EjemploApiHttpClient(IOptionsSnapshot<HttpClientOptions> options, IGenericHttpClient client) : base(client) =>
        _httpClientOptions = options.Get(nameof(EjemploApiHttpClient));

    public async Task<GenericResponseDto<string>> Post(HttpContext httpContext, CancellationToken token,
        Dictionary<string, string> defaultRequestHeaders = null, Dictionary<string, string> queryString = null, string data = null,
        FormFileDto formFileDto = null) =>
        await HttpClient.PostAsync<string, GenericResponseDto<string>>(
            nameof(Post), data, token, httpContext, _httpClientOptions, defaultRequestHeaders, queryString, formFileDto).ConfigureAwait(false);

    public async Task<MemoryStream> Get(HttpContext httpContext, CancellationToken token,
        Dictionary<string, string> defaultRequestHeaders = null, Dictionary<string, string> queryString = null) =>
        await HttpClient.GetAsync<MemoryStream>(
            nameof(Get), token, httpContext, _httpClientOptions, defaultRequestHeaders, queryString).ConfigureAwait(false);
}