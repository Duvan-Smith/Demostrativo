using System.Net;

namespace Demostrativo.Jwt.Aplication.Dto.Base;

public class RequestDtoBase : DataTransferObjectBase
{
    public HttpStatusCode StatusCode { get; set; }
    public string StatusDescription { get; set; } = default!;
}