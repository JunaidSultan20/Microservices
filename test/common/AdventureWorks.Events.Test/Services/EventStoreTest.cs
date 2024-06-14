using FluentAssertions;
using MongoDB.Bson;

namespace AdventureWorks.Events.Test.Services;

public class EventStoreTest : EventStoreTestData
{
    [Theory]
    [InlineData("test1-stream", "events")]
    [InlineData("test2-stream", "events")]
    private async Task When_Events_Created_Insert_Into_Database(string streamId, string collectionName)
    {
        // Arrange
        var sut = SetupMockClient().SetupMockOptions().Build();

        var aggregate = CreateAggregate();

        // Act
        await sut.SaveAsync(aggregate, streamId, collectionName);

        // Assert
        var count = await _collectionMock.Object.CountDocumentsAsync(new BsonDocument());

        count.Should().Be(ExpectedEvents.Count);
    }
}