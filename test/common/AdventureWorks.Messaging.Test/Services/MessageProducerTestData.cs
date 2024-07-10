using AdventureWorks.Common.Options;
using AdventureWorks.Messaging.Services;
using Microsoft.Extensions.Options;
using Moq;
using RabbitMQ.Client;

namespace AdventureWorks.Messaging.Test.Services;

public class MessageProducerTestData
{
    private readonly Mock<IOptionsMonitor<RabbitMqOptions>> _optionsMock;
    protected Mock<IConnectionFactory> _connectionFactoryMock;
    protected Mock<IConnection> _connectionMock;
    protected Mock<IChannel> _channelMock;

    protected MessageProducerTestData()
    {
        _optionsMock = new Mock<IOptionsMonitor<RabbitMqOptions>>();
        _connectionFactoryMock = new Mock<IConnectionFactory>();
        _connectionMock = new Mock<IConnection>();
        _channelMock = new Mock<IChannel>();
    }

    public MessageProducerTestData SetupMockProducer()
    {
        _connectionFactoryMock.Setup(x => x.CreateConnectionAsync(It.IsAny<CancellationToken>()))
                              .ReturnsAsync(_connectionMock.Object);
        //_connectionMock.Setup(x => x.CreateChannel()).Returns(_channelMock.);
        //try
        //{
        //    _connectionMock.SetupGet(x => x.CreateChannelAsync())
        //                   .Returns(ValueTask.FromResult(_channelMock.Object));
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}

        _channelMock.Setup(x => x.ExchangeDeclare(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                                                  It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()));

        _channelMock.Setup(x => x.QueueDeclare(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(),
                                               It.IsAny<IDictionary<string, object>>()));

        _channelMock.Setup(x => x.QueueBind(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                                            It.IsAny<IDictionary<string, object>>()));

        return this;
    }

    public MessageProducerTestData SetupMockOptions()
    {
        RabbitMqOptions options = new RabbitMqOptions()
        {
            Hostname = "test-host",
            Port = 1234,
            Username = "test-user",
            Password = "test-password"
        };

        _optionsMock.Setup(x => x.CurrentValue).Returns(options);

        return this;
    }

    public MessageProducer Build()
    {
        return new MessageProducer(_optionsMock.Object);
    }
}

internal class TestMessage
{
    public int Value { get; set; }

    public string? Message { get; set; }
}