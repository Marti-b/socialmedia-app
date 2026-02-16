using CQRS.Core.Commands;

namespace CQRS.Core.Events;

public abstract class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        this.Type = type;
    }
    // replaying the latest state of the aggregate
    public int Version { get; set; }
    // discriminator property
    public string Type { get; set; }
    
}