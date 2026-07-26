using AdventureWorks.Messaging.Services;
using Moq;
using RabbitMQ.Client;

namespace AdventureWorks.Messaging.Test.Services;

public class MessageProducerTestData
{
    protected Mock<IConnectionFactory> _connectionFactoryMock;
    protected Mock<IConnection> _connectionMock;
    protected Mock<IChannel> _channelMock;

    protected MessageProducerTestData()
    {
        _connectionFactoryMock = new Mock<IConnectionFactory>();
        _connectionMock = new Mock<IConnection>();
        _channelMock = new Mock<IChannel>();
    }

    public MessageProducerTestData SetupMockProducer()
    {
        _connectionFactoryMock
            .Setup(x => x.CreateConnectionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_connectionMock.Object);

        _connectionMock
            .Setup(x => x.CreateChannelAsync(It.IsAny<CreateChannelOptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_channelMock.Object);

        _channelMock
            .Setup(x => x.ExchangeDeclareAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<IDictionary<string, object?>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _channelMock
            .Setup(x => x.QueueDeclareAsync(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<IDictionary<string, object?>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((QueueDeclareOk)null!);

        _channelMock
            .Setup(x => x.QueueBindAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, object?>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _channelMock
            .Setup(x => x.BasicPublishAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<BasicProperties>(),
                It.IsAny<ReadOnlyMemory<byte>>(),
                It.IsAny<CancellationToken>()))
            .Returns(new ValueTask());

        return this;
    }

    public MessageProducer Build()
    {
        return new MessageProducer(_connectionFactoryMock.Object);
    }
}

internal class TestMessage
{
    public int Value { get; set; }

    public string? Message { get; set; }
}