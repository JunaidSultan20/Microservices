using AdventureWorks.Common.Events;
using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;

namespace AdventureWorks.Events.Test.Services;

public class EventStoreTestData
{
    private readonly Mock<IMongoClient> _clientMock;
    private readonly Mock<IMongoDatabase> _databaseMock;
    protected readonly Mock<IMongoCollection<BsonDocument>> _collectionMock;
    private readonly Mock<IOptionsMonitor<EventStoreOptions>> _eventStoreOptionsMock;
    protected List<BsonDocument> ExpectedEvents;

    public EventStoreTestData()
    {
        _clientMock = new Mock<IMongoClient>();
        _databaseMock = new Mock<IMongoDatabase>();
        _collectionMock = new Mock<IMongoCollection<BsonDocument>>();
        _eventStoreOptionsMock = new Mock<IOptionsMonitor<EventStoreOptions>>();
        ExpectedEvents = new List<BsonDocument>
        {
            new BsonDocument
            {
                { "type", "TestEvent1" },
                { "version", 1 },
                { "streamId", "testStream" },
                { "timeStamp", DateTime.Now },
                { "data", BsonDocument.Parse("{\"Value\":1}") }
            },
            new BsonDocument
            {
                { "type", "TestEvent2" },
                { "version", 2 },
                { "streamId", "testStream" },
                { "timeStamp", DateTime.Now },
                { "data", BsonDocument.Parse("{\"Value\":2}") }
            }
        };
    }

    public EventStoreTestData SetupMockClient()
    {
        _clientMock.Setup(x => x.GetDatabase(It.IsAny<string>(), null))
                   .Returns(_databaseMock.Object);

        _databaseMock.Setup(x => x.GetCollection<BsonDocument>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                     .Returns(_collectionMock.Object);

        _collectionMock.Setup(x => x.InsertManyAsync(It.IsAny<BsonDocument[]?>(), It.IsAny<InsertManyOptions>(), It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

        _collectionMock.Setup(x => x.CountDocumentsAsync(It.IsAny<FilterDefinition<BsonDocument>>(), It.IsAny<CountOptions>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(ExpectedEvents.Count);

        return this;
    }

    public EventStoreTestData SetupMockOptions()
    {
        EventStoreOptions options = new EventStoreOptions
        {
            ServerUri = "test-uri",
            Database = "test-database",
            Collection = "test-collection"
        };

        _eventStoreOptionsMock.Setup(x => x.CurrentValue).Returns(options);

        return this;
    }

    public TestAggregate CreateAggregate()
    {
        var aggregate = new TestAggregate();
        aggregate.Event1(10);
        aggregate.Event2(20);
        return aggregate;
    }

    public Events.Services.EventStore Build()
    {
        return new Events.Services.EventStore(_clientMock.Object, _eventStoreOptionsMock.Object);
    }
}

public class TestEvent1(int value)
{
    public int Value { get; set; } = value;
}

public class TestEvent2(int value)
{
    public int Value { get; set; } = value;
}

public class TestAggregate : Aggregate
{
    public int Value { get; set; }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case TestEvent1 e:
                OnEvent1(e);
                break;
            case TestEvent2 e:
                OnEvent2(e);
                break;
            default:
                throw new NotSupportedException($"Event {@event.GetType().Name} is not supported.");
        }
    }

    public void Event1(int value) => Apply(new TestEvent1(value));

    public void Event2(int value) => Apply(new TestEvent2(value));

    private void OnEvent1(TestEvent1 @event) => Value = @event.Value;

    private void OnEvent2(TestEvent2 @event) => Value = @event.Value;
}