namespace AdventureWorks.Contracts.EventStreaming;

/// <summary>
/// Represents the root entity of an aggregate in a domain-driven design context.
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    /// Gets the version of the aggregate, typically used for concurrency control.
    /// </summary>
    long Version { get; }

    /// <summary>
    /// Gets the collection of domain events associated with the aggregate.
    /// These events represent state changes in the aggregate.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> Events { get; }

    /// <summary>
    /// Clears the domain events from the aggregate.
    /// Typically called after the events have been processed or published.
    /// </summary>
    void ClearEvents();
}
