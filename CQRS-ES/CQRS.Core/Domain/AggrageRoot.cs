using CQRS.Core.Commands;

namespace CQRS.Core.Domain;

public abstract class AggrageRoot
{
    protected Guid _id;
    private readonly List<BaseEvent> _changes = new();
    public Guid Id => _id;
    public int Version { get; set; } = -1;
    public IEnumerable<BaseEvent> GetUncommittedChanges()
    {
        return _changes;
    }

    public void MarkChangesAsCommitted()
    {
        _changes.Clear();
    }

    private void ApplyChange(BaseEvent @event, bool isNew)
    {
        var method = this.GetType()
            .GetMethod("Apply", new [] { @event.GetType() });
        if (method == null)
        {
            throw new ArgumentException(nameof(method), $"The Apply method was not found in the aggragete for {@event.GetType().Name}");
        }
        
        method.Invoke(this, new object[] { @event });
        
        if (isNew)
        {
            _changes.Add(@event);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    public void RePlayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChange(@event, false);
        }
    }
}