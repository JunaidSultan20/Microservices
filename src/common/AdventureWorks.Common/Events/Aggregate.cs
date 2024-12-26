namespace AdventureWorks.Common.Events;

/// <summary>
/// Represents an abstract base class for an aggregate that tracks changes and maintains its state based on events.
/// </summary>
public abstract class Aggregate
{
    private readonly IList<object> _changes = new List<object>();

    /// <summary>
    /// Gets or sets the unique identifier for the aggregate.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the version of the aggregate.
    /// </summary>
    public long Version { get; set; }

    /// <summary>
    /// Applies the given event to the aggregate state.
    /// This method must be implemented in derived classes to update the state based on the event.
    /// </summary>
    /// <param name="event">The event to apply to the aggregate.</param>
    protected abstract void When(object @event);

    /// <summary>
    /// Applies the given event and stores it as a change to be persisted later.
    /// </summary>
    /// <param name="event">The event to apply to the aggregate and track.</param>
    protected void Apply(object @event)
    {
        When(@event);
        _changes.Add(@event);
    }

    /// <summary>
    /// Loads the aggregate's state from its history of events and sets its version.
    /// </summary>
    /// <param name="version">The version of the aggregate to set.</param>
    /// <param name="history">A sequence of events that represent the history of the aggregate.</param>
    public void Load(long version, IEnumerable<object> history)
    {
        Version = version;
        foreach (var e in history)
        {
            When(e);
        }
    }

    /// <summary>
    /// Gets the changes that have been applied to the aggregate since it was last loaded.
    /// </summary>
    /// <returns>An array of events representing the changes.</returns>
    public object[] GetChanges() => _changes.ToArray();
}