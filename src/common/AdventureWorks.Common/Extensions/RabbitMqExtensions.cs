using RabbitMQ.Client;

namespace AdventureWorks.Common.Extensions;

public static class RabbitMqExtensions
{
    /// <summary>
    /// Connection factory extension method that takes RabbitMq configurations and returns the factory connection
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="options"></param>
    /// <returns></returns>
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