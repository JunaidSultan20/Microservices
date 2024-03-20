using Microsoft.Extensions.Options;

namespace AdventureWorks.Messaging.Services;

public class MessageReceiver(IOptionsMonitor<RabbitMqOptions> options) : IMessageReceiver
{
    private readonly RabbitMqOptions _options = options.CurrentValue;
    public void ReceiveMessage<T>(string queue,
                                  string exchangeName,
                                  string exchangeType,
                                  string routeKey)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Hostname,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateChannel();

        channel.ExchangeDeclare(exchangeName, exchangeType);

        channel.QueueDeclare(queue: queue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        channel.QueueBind(queue: queue, exchange: exchangeName, routeKey);
    }
}