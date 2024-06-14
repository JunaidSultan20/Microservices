namespace AdventureWorks.Messaging.Test.Services;

public class MessageProducerTest : MessageProducerTestData
{
    [Theory]
    [InlineData("test-queue", "test-exchange", "test-exchange-type", "test-route-key")]
    public async Task When_Message_Is_Produced_Expect_Message_Pushed_To_Queue(string queue, string exchangeName, string exchangeType, string routeKey)
    {
        // Arrange
        var producer = SetupMockProducer().SetupMockOptions().Build();

        var message = new TestMessage { Message = "message", Value = 20 };

        // Act
        await producer.SendMessageAsync(queue, exchangeName, exchangeType, routeKey, message);

        // Assert
        _connectionFactoryMock.VerifyAll();
        _connectionMock.VerifyAll();
        _channelMock.VerifyAll();
    }
}