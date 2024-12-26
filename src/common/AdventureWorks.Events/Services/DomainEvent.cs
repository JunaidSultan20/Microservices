using AdventureWorks.Contracts.EventStreaming;

namespace AdventureWorks.Events.Services;

/// <summary>
/// Represents the base class for domain events, implementing the <see cref="IDomainEvent"/> interface.
/// A domain event signifies something important that has occurred in the domain.
/// </summary>
public class DomainEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// </summary>
    public DomainEvent()
    {
    }

    /// <summary>
    /// Gets the version of the aggregate at the time the event occurred.
    /// This is used for concurrency control or event sourcing.
    /// </summary>
    public long AggregateVersion { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the aggregate associated with the event.
    /// </summary>
    public string AggregateId { get; private set; }

    /// <summary>
    /// Gets the timestamp indicating when the event occurred.
    /// </summary>
    public DateTime TimeStamp { get; private set; }
}