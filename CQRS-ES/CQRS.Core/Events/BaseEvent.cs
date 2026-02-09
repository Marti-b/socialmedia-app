namespace CQRS.Core.Commands;

public abstract class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        this.Type = type;
    }
    // replaying the latest state of the aggrage
    public int Version { get; set; }
    // discriminator property
    public string Type { get; set; }
    
}