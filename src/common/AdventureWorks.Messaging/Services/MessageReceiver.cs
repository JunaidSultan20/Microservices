using Microsoft.Extensions.Options;

namespace AdventureWorks.Messaging.Services;

public class MessageReceiver(IConnectionFactory connectionFactory) : IMessageReceiver
{
    public async Task ReceiveMessage<T>(string queue,
                                  string exchangeName,
                                  string exchangeType,
                                  string routeKey)
    {
        using IConnection connection = await connectionFactory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchangeName, exchangeType);
        await channel.QueueDeclareAsync(queue: queue,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
        await channel.QueueBindAsync(queue: queue, exchange: exchangeName, routeKey);
    }
}