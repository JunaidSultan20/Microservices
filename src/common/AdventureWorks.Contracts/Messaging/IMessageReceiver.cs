namespace AdventureWorks.Contracts.Messaging;

/// <summary>
/// Represents a message receiver responsible for receiving messages from a message queue or broker.
/// </summary>
public interface IMessageReceiver
{
    /// <summary>
    /// Receives messages from the specified queue using the provided exchange details.
    /// </summary>
    /// <typeparam name="T">The type of the message to be received.</typeparam>
    /// <param name="queue">The name of the queue from which the message will be received.</param>
    /// <param name="exchangeName">The name of the exchange that will route the message.</param>
    /// <param name="exchangeType">The type of the exchange (e.g., direct, topic, fan-out).</param>
    /// <param name="routeKey">The routing key used by the exchange to determine which queue(s) should receive the message.</param>
    Task ReceiveMessage<T>(string queue,
                           string exchangeName,
                           string exchangeType,
                           string routeKey);
}