namespace AdventureWorks.Contracts.Messaging;

public interface IMessageProducer
{
    Task SendMessageAsync<T>(string queue, 
                             string exchangeName, 
                             string exchangeType, 
                             string routeKey, 
                             T message);
}