using Demostrativo.Jwt.Cross.Authorization.Extensions;
using Demostrativo.Jwt.Cross.Http.Configuration;
using Demostrativo.Jwt.Cross.Http.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Demostrativo.Jwt.Cross.Http.Base;

public enum RequestMethod
{
    GET, HEAD, POST, PUT, PATCH, DELETE, OPTIONS, TRACE
}

public class GenericHttpClient : IGenericHttpClient
{
    private readonly HttpClient _client;

    public GenericHttpClient(HttpClient client) =>
        _client = client ?? throw new ClientNotSpecifiedException();

    public async Task<TResponse> GetAsync<TResponse>(
        string path, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null,
        Dictionary<string, string>? defaultRequestHeaders = null,
        Dictionary<string, string>? queryString = null) =>
        await ProcessResponse<TResponse>(path, RequestMethod.GET, token, httpContext, null, options, defaultRequestHeaders, queryString).ConfigureAwait(false);

    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string path, TRequest request, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null,
        Dictionary<string, string>? defaultRequestHeaders = null,
        Dictionary<string, string>? queryString = null,
        FormFileDto formFileDto = null) =>
        await ProcessResponse<TResponse>(path, RequestMethod.POST, token, httpContext, request, options, defaultRequestHeaders, queryString, formFileDto).ConfigureAwait(false);

    public async Task<TResponse> PatchAsync<TRequest, TResponse>(
        string path, TRequest request, CancellationToken token, HttpContext httpContext = null, HttpClientOptions options = null,
        Dictionary<string, string> defaultRequestHeaders = null,
        Dictionary<string, string> queryString = null,
        FormFileDto formFileDto = null) =>
        await ProcessResponse<TResponse>(path, RequestMethod.PATCH, token, httpContext, request, options, defaultRequestHeaders, queryString).ConfigureAwait(false);

    public async Task<TResponse> PutAsync<TRequest, TResponse>(
        string path, TRequest request, CancellationToken token, HttpContext httpContext = null, HttpClientOptions options = null,
        Dictionary<string, string> defaultRequestHeaders = null,
        Dictionary<string, string> queryString = null,
        FormFileDto formFileDto = null) =>
        await ProcessResponse<TResponse>(path, RequestMethod.PUT, token, httpContext, request, options, defaultRequestHeaders, queryString).ConfigureAwait(false);

    public async Task<TResponse> DeleteAsync<TResponse>(
        string path, string id, CancellationToken token, HttpContext httpContext = null, HttpClientOptions options = null) =>
        await ProcessResponse<TResponse>($"{path}/{id}", RequestMethod.DELETE, token, httpContext, options).ConfigureAwait(false);

    #region Helpers
    private async Task<TResponse> ProcessResponse<TResponse>(
        string path, RequestMethod method, CancellationToken token, HttpContext httpContext = null, object request = null, HttpClientOptions options = null,
        Dictionary<string, string> defaultRequestHeaders = null,
        Dictionary<string, string> queryString = null,
        FormFileDto formFileDto = null)
    {
        var response = await ProcessRequest(path, method, token, httpContext, request, options, defaultRequestHeaders, queryString, formFileDto).ConfigureAwait(false);
        var flatResponse = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);

        if (typeof(TResponse) == typeof(MemoryStream))
        {
            await using var memoryStream = new MemoryStream();
            try
            {
                await response.Content.CopyToAsync(memoryStream);
                return (TResponse)Convert.ChangeType(memoryStream, typeof(TResponse));
            }
            catch (Exception e)
            {
                memoryStream.Dispose();
                throw;
            }
            finally
            {
                memoryStream.Dispose();
            }
        }

        return typeof(TResponse) == typeof(string)
            ? (TResponse)Convert.ChangeType(flatResponse, typeof(TResponse))
            : flatResponse == "{}" || flatResponse == "[]" || flatResponse == ""
            ? default
            : JsonConvert.DeserializeObject<TResponse>(flatResponse);
    }

    private async Task<HttpResponseMessage> ProcessRequest(
        string path, RequestMethod method, CancellationToken token, HttpContext httpContext = null, object request = null, HttpClientOptions options = null,
        Dictionary<string, string> defaultRequestHeaders = null,
        Dictionary<string, string> queryString = null,
        FormFileDto formFileDto = null)
    {
        var jwt = string.Empty;

        if (string.IsNullOrEmpty(jwt) && httpContext != null)
            jwt = httpContext.ExtractJwt();

        if (!string.IsNullOrEmpty(jwt))
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        var stringRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var content = new MultipartFormDataContent();
        if (formFileDto != null && formFileDto.Stream != null && formFileDto.IFormFile != null && formFileDto.Path != null)
        {
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            content.Add(new StreamContent(formFileDto.Stream, (int)formFileDto.Stream.Length), formFileDto.Path, formFileDto.IFormFile.FileName);
        }

        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (defaultRequestHeaders != null && defaultRequestHeaders.Count != 0)
        {
            foreach (var header in defaultRequestHeaders)
            {
                try
                {
                    var values = _client.DefaultRequestHeaders.GetValues(header.Key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            if (header.Value != value)
                                _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        var getUrl = options.Port == 0 ? options.GetUrlNullPort() : options.GetUrl();

        if (queryString != null && queryString.Count != 0)
            path = QueryHelpers.AddQueryString(path, queryString);

        var response = method switch
        {
            RequestMethod.GET => await _client.GetAsync($"{getUrl}/{path}", token).ConfigureAwait(false),
            RequestMethod.POST => await _client.PostAsync($"{getUrl}/{path}",
            formFileDto != null && formFileDto.Stream != null && formFileDto.IFormFile != null ? content : stringRequest, token).ConfigureAwait(false),
            RequestMethod.PUT => await _client.PutAsync($"{getUrl}/{path}",
            formFileDto != null && formFileDto.Stream != null && formFileDto.IFormFile != null ? content : stringRequest, token).ConfigureAwait(false),
            RequestMethod.PATCH => await _client.PatchAsync($"{getUrl}/{path}",
            formFileDto != null && formFileDto.Stream != null && formFileDto.IFormFile != null ? content : stringRequest, token).ConfigureAwait(false),
            RequestMethod.DELETE => await _client.DeleteAsync($"{getUrl}/{path}", token).ConfigureAwait(false),
            RequestMethod.HEAD or RequestMethod.OPTIONS or RequestMethod.TRACE => throw new NotSupportedException(),
            _ => throw new ArgumentNullException(nameof(method)),
        };

        response.EnsureSuccessStatusCode();
        return response;
    }
    #endregion Helpers
}