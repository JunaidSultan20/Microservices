namespace AdventureWorks.Contracts.Messaging;

public interface IMessageProducer
{
    void SendMessage<T>(string queue,
                        string exchangeName,
                        string exchangeType,
                        string routeKey,
                        T message);
}