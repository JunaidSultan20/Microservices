using AdventureWorks.Common.Events;

namespace AdventureWorks.Contracts.EventStreaming;

public interface IEventStore
{
    public Task SaveAsync<T>(T aggregate, string streamId, string collectionName) where T : Aggregate, new();

    public Task<T> LoadAsync<T>(Guid aggregateId) where T : Aggregate, new();
}