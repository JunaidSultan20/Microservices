namespace AdventureWorks.Contracts.EventStreaming;

/// <summary>
/// Represents a domain event in a domain-driven design context. 
/// A domain event is something that has occurred in the domain that is worth noting.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the version of the aggregate at the time the event occurred.
    /// This is typically used for concurrency control or event sourcing.
    /// </summary>
    long AggregateVersion { get; }

    /// <summary>
    /// Gets the unique identifier of the aggregate associated with the event.
    /// </summary>
    string AggregateId { get; }

    /// <summary>
    /// Gets the timestamp indicating when the event occurred.
    /// </summary>
    DateTime TimeStamp { get; }
}