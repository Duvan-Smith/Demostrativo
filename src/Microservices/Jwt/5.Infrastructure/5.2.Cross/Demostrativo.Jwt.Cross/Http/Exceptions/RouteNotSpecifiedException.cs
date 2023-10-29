namespace Demostrativo.Jwt.Cross.Http.Exceptions;

public class RouteNotSpecifiedException : Exception
{
    public RouteNotSpecifiedException()
    {
    }

    public RouteNotSpecifiedException(string message) : base(message)
    {
    }

    public RouteNotSpecifiedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}