using AdventureWorks.Common.Options;
using AdventureWorks.Middlewares.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AdventureWorks.Middlewares.Test.Logging;

public class RequestLoggingMiddlewareTestData
{
    protected readonly Mock<RequestDelegate> _mockNext;
    private readonly Mock<IMongoClient> _mockClient;
    private readonly Mock<IMongoDatabase> _mockDatabase;
    protected readonly Mock<IMongoCollection<BsonDocument>> _mockCollection;
    private readonly Mock<IOptionsMonitor<RequestLogOptions>> _mockOptions;

    protected RequestLoggingMiddlewareTestData()
    {
        _mockNext = new Mock<RequestDelegate>();
        _mockClient = new Mock<IMongoClient>();
        _mockDatabase = new Mock<IMongoDatabase>();
        _mockCollection = new Mock<IMongoCollection<BsonDocument>>();
        _mockOptions = new Mock<IOptionsMonitor<RequestLogOptions>>();
    }

    protected RequestLoggingMiddlewareTestData SetupMockClient()
    {
        _mockClient.Setup(x => x.GetDatabase(It.IsAny<string>(), null))
                   .Returns(_mockDatabase.Object);

        _mockDatabase.Setup(x => x.GetCollection<BsonDocument>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                     .Returns(_mockCollection.Object);

        _mockCollection.Setup(x => x.InsertOneAsync(It.IsAny<BsonDocument>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

        return this;
    }

    protected internal RequestLoggingMiddlewareTestData SetupMockOptions()
    {
        RequestLogOptions options = new RequestLogOptions
        {
            Collection = "test-collection",
            Database = "test-database",
            ServerUri = "test-uri"
        };

        _mockOptions.Setup(x => x.CurrentValue).Returns(options);
        
        return this;
    }

    protected HttpContext CreateContext()
    {
        return new DefaultHttpContext();
    }

    public RequestLoggingMiddleware Build()
    {
        return new RequestLoggingMiddleware(_mockNext.Object, _mockClient.Object, _mockOptions.Object);
    }
}