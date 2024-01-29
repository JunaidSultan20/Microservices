namespace AdventureWorks.Messaging.Services;

public class MessageProducer : IMessageProducer
{
    public void SendMessage<T>(RabbitMqOptions queueConfig,
                               string queue,
                               string exchangeName,
                               string exchangeType,
                               string routeKey,
                               T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = queueConfig.Hostname,
            Port = queueConfig.Port,
            UserName = queueConfig.Username,
            Password = queueConfig.Password
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateChannel();

        channel.ExchangeDeclare(exchangeName, exchangeType);

        channel.QueueDeclare(queue: queue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.QueueBind(queue: queue, exchange: exchangeName, routeKey);

        var json = JsonConvert.SerializeObject(message);

        var body = Encoding.UTF8.GetBytes(json);

        //var properties = channel.CreateBasicProperties();

        //properties.Persistent = true;

        channel.BasicPublish(exchange: exchangeName,
                             routingKey: routeKey,
                             body: body);
    }
}