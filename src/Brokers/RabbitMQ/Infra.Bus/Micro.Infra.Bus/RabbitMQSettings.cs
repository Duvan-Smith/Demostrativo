namespace Micro.Infra.Bus;

public class RabbitMQSettings
{
    public string Hostname { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;

    public void CopyFrom(RabbitMQSettings settings)
    {
        Hostname = settings.Hostname;
        Username = settings.Username;
        Password = settings.Password;
    }
}