using AdventureWorks.Common.Events;
using AdventureWorks.Common.Options;
using AdventureWorks.Contracts.EventStreaming;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace AdventureWorks.Events.Services;

/// <summary>
/// Represents an event store for saving and loading aggregate events using MongoDB.
/// Implements the <see cref="IEventStore"/> interface.
/// </summary>
/// <param name="client">The MongoDB client used to connect to the MongoDB database.</param>
/// <param name="options">The configuration options for the event store.</param>
public class EventStore([FromKeyedServices("EventStore")] IMongoClient client, 
                        IOptionsMonitor<EventStoreOptions> options) : IEventStore
{
    private readonly EventStoreOptions _options = options.CurrentValue;

    /// <summary>
    /// Saves the events of the specified aggregate asynchronously to the MongoDB event store.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate.</typeparam>
    /// <param name="aggregate">The aggregate containing the events to be saved.</param>
    /// <param name="streamId">The stream ID associated with the aggregate events.</param>
    /// <param name="collectionName">The name of the MongoDB collection to store the events.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    public async Task SaveAsync<T>(T aggregate, string streamId, string collectionName) where T : Aggregate, new()
    {
        BsonDocument[] events = aggregate
                                 .GetChanges()
                                 .Select(@event => new BsonDocument 
                                 {
                                     { "type", @event.GetType().Name }, 
                                     { "version", ++aggregate.Version }, 
                                     { "streamId", streamId }, 
                                     { "timeStamp", DateTime.Now },
                                     { "data", BsonDocument.Parse(JsonConvert.SerializeObject(@event)) }
                                 }).ToArray();

        IMongoDatabase database = client.GetDatabase(_options.Database);
        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);

        await collection.InsertManyAsync(events);
    }

    /// <summary>
    /// Loads the aggregate by its ID asynchronously from the MongoDB event store.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate.</typeparam>
    /// <param name="aggregateId">The unique identifier of the aggregate to be loaded.</param>
    /// <returns>A task representing the asynchronous load operation, which returns the loaded aggregate.</returns>
    public async Task<T> LoadAsync<T>(Guid aggregateId) where T : Aggregate, new()
    {
        throw new NotImplementedException();
    }
}