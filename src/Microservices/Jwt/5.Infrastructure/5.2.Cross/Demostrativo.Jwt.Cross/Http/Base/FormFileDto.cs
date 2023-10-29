using Microsoft.AspNetCore.Http;

namespace Demostrativo.Jwt.Cross.Http.Base;

public class FormFileDto
{
    public Stream Stream { get; set; } = default!;
    public IFormFile IFormFile { get; set; } = default!;
    public string Path { get; set; } = default!;
}