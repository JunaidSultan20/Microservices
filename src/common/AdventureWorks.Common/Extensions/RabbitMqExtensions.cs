using RabbitMQ.Client;

namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring RabbitMQ connection factories.
/// </summary>
public static class RabbitMqExtensions
{
    /// <summary>
    /// Configures and creates a <see cref="ConnectionFactory"/> instance using the specified RabbitMQ options.
    /// </summary>
    /// <param name="factory">The <see cref="ConnectionFactory"/> instance to configure.</param>
    /// <param name="options">The RabbitMQ configuration options to use for setting up the connection factory.</param>
    /// <returns>The configured <see cref="ConnectionFactory"/> instance.</returns>
    public static ConnectionFactory CreateConnection(this ConnectionFactory factory, RabbitMqOptions options)
    {
        factory = new ConnectionFactory
        {
            HostName = options.Hostname,
            Port = options.Port,
            UserName = options.Username,
            Password = options.Password
        };

        return factory;
    }
}