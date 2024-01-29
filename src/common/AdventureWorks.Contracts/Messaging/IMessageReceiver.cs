namespace AdventureWorks.Contracts.Messaging;

public interface IMessageReceiver
{
    void ReceiveMessage<T>(RabbitMqOptions queueConfig,
                           string queue,
                           string exchangeName,
                           string exchangeType,
                           string routeKey);
}