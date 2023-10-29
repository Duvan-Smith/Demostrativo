using Micro.Domain.Core.Events;

namespace Micro.Domain.Core.Commands;

public abstract class Command : Message
{
    public DateTime Timestamp { get; protected set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}