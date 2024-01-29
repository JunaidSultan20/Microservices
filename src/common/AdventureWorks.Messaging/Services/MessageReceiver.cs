namespace AdventureWorks.Messaging.Services;

public class MessageReceiver : IMessageReceiver
{
    public void ReceiveMessage<T>(RabbitMqOptions queueConfig,
                                  string queue,
                                  string exchangeName,
                                  string exchangeType,
                                  string routeKey)
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

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        channel.QueueBind(queue: queue, exchange: exchangeName, routeKey);
    }
}