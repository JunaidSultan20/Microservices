namespace AdventureWorks.Contracts.Messaging;

/// <summary>
/// Represents a message producer responsible for sending messages to a message queue or broker.
/// </summary>
public interface IMessageProducer
{
    /// <summary>
    /// Asynchronously sends a message to the specified queue using the provided exchange details.
    /// </summary>
    /// <typeparam name="T">The type of the message to be sent.</typeparam>
    /// <param name="queue">The name of the queue to which the message will be sent.</param>
    /// <param name="exchangeName">The name of the exchange that will route the message.</param>
    /// <param name="exchangeType">The type of the exchange (e.g., direct, topic, fanout).</param>
    /// <param name="routeKey">The routing key used by the exchange to determine which queue(s) should receive the message.</param>
    /// <param name="message">The message to be sent.</param>
    /// <returns>A task representing the asynchronous operation of sending the message.</returns>
    Task SendMessageAsync<T>(string queue,
                             string exchangeName,
                             string exchangeType,
                             string routeKey,
                             T message);
}