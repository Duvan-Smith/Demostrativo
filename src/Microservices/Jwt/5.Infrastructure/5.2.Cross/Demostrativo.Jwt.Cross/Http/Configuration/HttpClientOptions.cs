using Demostrativo.Jwt.Aplication.Dto.Base;

namespace Demostrativo.Jwt.Cross.Http.Configuration;

public class HttpClientOptions : DataTransferObjectBase
{
    public string ServiceProtocol { get; set; }
    public string Hostname { get; set; }
    public int Port { get; set; }
    public string Context { get; set; }
    public string Controller { get; set; }

    public void CopyFrom(HttpClientOptions parameters)
    {
        ServiceProtocol = parameters.ServiceProtocol;
        Port = parameters.Port;
        Context = parameters.Context;
        Hostname = parameters.Hostname;
        Controller = parameters.Controller;
    }

    public Uri GetUrl() => new UriBuilder
    {
        Host = Hostname,
        Port = Port,
        Path = Context + GetController(),
        Scheme = ServiceProtocol
    }.Uri;

    public Uri GetUrlNullPort() => new UriBuilder
    {
        Host = Hostname,
        Path = Context + GetController(),
        Scheme = ServiceProtocol
    }.Uri;

    public string GetController() => string.IsNullOrEmpty(Controller) ? "" : "/" + Controller;
}