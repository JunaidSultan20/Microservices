using Microsoft.Extensions.Options;

namespace AdventureWorks.Messaging.Services;

public class MessageProducer(IOptionsMonitor<RabbitMqOptions> options) : IMessageProducer
{
    private readonly RabbitMqOptions _options = options.CurrentValue;

    public async Task SendMessageAsync<T>(string queue, 
                                          string exchangeName, 
                                          string exchangeType, 
                                          string routeKey, 
                                          T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Hostname,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        using var connection = await factory.CreateConnectionAsync();

        using var channel = connection.CreateChannel();

        await channel.ExchangeDeclareAsync(exchangeName, exchangeType);

        await channel.QueueDeclareAsync(queue: queue, 
                                        durable: true, 
                                        exclusive: false, 
                                        autoDelete: false, 
                                        arguments: null);

        await channel.QueueBindAsync(queue: queue, 
                                     exchange: exchangeName, 
                                     routeKey);

        var json = JsonConvert.SerializeObject(message);

        var body = Encoding.UTF8.GetBytes(json);

        //var properties = channel.CreateBasicProperties();

        //properties.Persistent = true;

        await channel.BasicPublishAsync(exchange: exchangeName, 
                                        routingKey: routeKey, 
                                        body: body);
    }
}