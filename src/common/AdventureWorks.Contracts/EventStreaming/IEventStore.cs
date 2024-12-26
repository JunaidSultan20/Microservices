using AdventureWorks.Common.Events;

namespace AdventureWorks.Contracts.EventStreaming;

/// <summary>
/// Represents an event store for persisting and retrieving events 
/// associated with aggregates in a domain-driven design context.
/// </summary>
public interface IEventStore
{
    /// <summary>
    /// Asynchronously saves the events of an aggregate to the event store.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate to be saved.</typeparam>
    /// <param name="aggregate">The aggregate containing the events to be saved.</param>
    /// <param name="streamId">The identifier for the event stream (typically unique per aggregate).</param>
    /// <param name="collectionName">The name of the collection where the events will be stored.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    public Task SaveAsync<T>(T aggregate, string streamId, string collectionName) where T : Aggregate, new();

    /// <summary>
    /// Asynchronously loads an aggregate and its associated events from the event store.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate to be loaded.</typeparam>
    /// <param name="aggregateId">The unique identifier of the aggregate to be loaded.</param>
    /// <returns>A task that represents the asynchronous load operation, returning the loaded aggregate.</returns>
    public Task<T> LoadAsync<T>(Guid aggregateId) where T : Aggregate, new();
}