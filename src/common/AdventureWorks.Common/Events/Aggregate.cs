namespace AdventureWorks.Common.Events;

public abstract class Aggregate
{
    private readonly IList<object> _changes = new List<object>();

    public Guid Id { get; set; } = Guid.Empty;

    public long Version { get; set; } = 0;

    protected abstract void When(object @event);

    protected void Apply(object @event)
    {
        When(@event);

        _changes.Add(@event);
    }

    public void Load(long version, IEnumerable<object> history)
    {
        Version = version;

        foreach (var e in history)
        {
            When(e);
        }
    }

    public object[] GetChanges() => _changes.ToArray();
}