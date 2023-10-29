using Demostrativo.Jwt.Cross.Http.Configuration;
using Microsoft.AspNetCore.Http;

namespace Demostrativo.Jwt.Cross.Http.Base;

public interface IGenericHttpClient
{
    Task<TResponse> GetAsync<TResponse>(
        string path, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null,
        Dictionary<string, string>? defaultRequestHeaders = null,
        Dictionary<string, string>? queryString = null);

    Task<TResponse> PostAsync<TRequest, TResponse>(
        string path, TRequest request, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null,
        Dictionary<string, string>? defaultRequestHeaders = null,
        Dictionary<string, string>? queryString = null,
        FormFileDto? FormFileDto = null);

    Task<TResponse> PutAsync<TRequest, TResponse>(
        string path, TRequest request, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null,
        Dictionary<string, string>? defaultRequestHeaders = null,
        Dictionary<string, string>? queryString = null,
        FormFileDto? formFileDto = null);

    Task<TResponse> PatchAsync<TRequest, TResponse>(
        string path, TRequest request, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null,
        Dictionary<string, string>? defaultRequestHeaders = null,
        Dictionary<string, string>? queryString = null,
        FormFileDto? formFileDto = null);

    Task<TResponse> DeleteAsync<TResponse>(
        string path, string id, CancellationToken token, HttpContext? httpContext = null, HttpClientOptions? options = null);
}