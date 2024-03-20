namespace AdventureWorks.Contracts.Messaging;

public interface IMessageReceiver
{
    void ReceiveMessage<T>(string queue,
                           string exchangeName,
                           string exchangeType,
                           string routeKey);
}