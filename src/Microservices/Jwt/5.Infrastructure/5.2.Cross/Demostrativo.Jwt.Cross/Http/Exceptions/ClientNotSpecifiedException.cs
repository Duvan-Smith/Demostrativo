namespace Demostrativo.Jwt.Cross.Http.Exceptions;

public class ClientNotSpecifiedException : Exception
{
    public ClientNotSpecifiedException()
    {
    }

    public ClientNotSpecifiedException(string message) : base(message)
    {
    }

    public ClientNotSpecifiedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}