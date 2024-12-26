using Microsoft.Extensions.Options;

namespace AdventureWorks.Messaging.Services;

/// <summary>
/// Represents a message producer for sending messages to RabbitMQ exchanges and queues.
/// Implements the <see cref="IMessageProducer"/> interface.
/// </summary>
/// <param name="options">The configuration options for RabbitMQ.</param>
public class MessageProducer(IOptionsMonitor<RabbitMqOptions> options) : IMessageProducer
{
    private readonly RabbitMqOptions _options = options.CurrentValue;

    /// <summary>
    /// Asynchronously sends a message to the specified RabbitMQ queue using the provided exchange and routing details.
    /// </summary>
    /// <typeparam name="T">The type of the message to be sent.</typeparam>
    /// <param name="queue">The name of the queue to which the message will be sent.</param>
    /// <param name="exchangeName">The name of the exchange to use for routing the message.</param>
    /// <param name="exchangeType">The type of the exchange (e.g., direct, topic, fanout).</param>
    /// <param name="routeKey">The routing key used by the exchange to route the message to the appropriate queue.</param>
    /// <param name="message">The message to be sent.</param>
    /// <returns>A task representing the asynchronous operation of sending the message.</returns>
    public async Task SendMessageAsync<T>(string queue, 
                                          string exchangeName, 
                                          string exchangeType, 
                                          string routeKey, 
                                          T message)
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = _options.Hostname,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = connection.CreateChannel();
        
        await channel.ExchangeDeclareAsync(exchangeName, exchangeType);
        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        await channel.QueueBindAsync(queue: queue, exchange: exchangeName, routeKey);
        
        string json = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(json);

        // Uncomment and configure properties if needed
        // var properties = channel.CreateBasicProperties();
        // properties.Persistent = true;

        await channel.BasicPublishAsync(exchange: exchangeName, routingKey: routeKey, body: body);
    }
}