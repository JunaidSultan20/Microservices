using System.Text;
using AdventureWorks.Common.Events;
using AdventureWorks.Common.Options;
using AdventureWorks.Contracts.EventStreaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace AdventureWorks.Events.Services;

public class EventStore(IMongoClient client) : IEventStore
{
    public async Task SaveAsync<T>(T aggregate, string streamId, string collectionName) where T : Aggregate, new()
    {
        //var events = aggregate.GetChanges()
        //                             .Select(@event => new Event(id: Guid.NewGuid().ToString(), 
        //                                                         type: @event.GetType().Name, 
        //                                                         version: ++aggregate.Version, 
        //                                                         streamId: streamId, 
        //                                                         data: JsonConvert.SerializeObject(@event)))
        //                             .ToArray();

        var events = aggregate
                                 .GetChanges()
                                 .Select(@event => 
                                             new BsonDocument
                                             {
                                                 { "type", @event.GetType().Name }, 
                                                 { "version", ++aggregate.Version }, 
                                                 { "streamId", streamId }, 
                                                 { "data", BsonDocument.Parse(JsonConvert.SerializeObject(@event)) }
                                             }).ToArray();

        //IMongoDatabase database = client.GetDatabase(eventStoreOptions.Database);

        //IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(eventStoreOptions.Collection);

        //await collection.InsertManyAsync(events);
    }

    public async Task<T> LoadAsync<T>(Guid aggregateId) where T : Aggregate, new()
    {
        throw new NotImplementedException();
    }
}