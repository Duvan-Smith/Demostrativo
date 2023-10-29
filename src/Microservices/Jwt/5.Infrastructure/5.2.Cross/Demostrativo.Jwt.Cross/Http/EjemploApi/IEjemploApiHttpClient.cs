using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Cross.Http.Base;
using Microsoft.AspNetCore.Http;

namespace Demostrativo.Jwt.Cross.Http.EjemploApi;

public interface IEjemploApiHttpClient
{
    Task<GenericResponseDto<string>> Post(HttpContext httpContext, CancellationToken token,
        Dictionary<string, string> defaultRequestHeaders = null, Dictionary<string, string> queryString = null, string data = null,
        FormFileDto formFileDto = null);

    Task<MemoryStream> Get(HttpContext httpContext, CancellationToken token,
        Dictionary<string, string> defaultRequestHeaders = null, Dictionary<string, string> queryString = null);
}