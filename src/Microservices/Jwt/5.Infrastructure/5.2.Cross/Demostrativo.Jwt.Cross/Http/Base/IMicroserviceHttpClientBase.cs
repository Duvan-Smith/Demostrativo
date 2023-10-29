namespace Demostrativo.Jwt.Cross.Http.Base;

public interface IMicroserviceHttpClientBase
{
    Task<bool> CheckHealt();
}