using Microsoft.AspNetCore.Http;

namespace Demostrativo.Jwt.Cross.Authorization.Extensions;

public static class HttpContextExtension
{
    public static string? ExtractJwt(this HttpContext context)
    {
        context.Request.Headers.TryGetValue("Authorization", out var jwt);
        jwt = jwt.ToString().Replace("Bearer", string.Empty);
        return !string.IsNullOrEmpty(jwt) ? jwt : default;
    }
}