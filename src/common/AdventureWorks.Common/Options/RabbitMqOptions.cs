namespace AdventureWorks.Common.Options;

/// <summary>
/// Represents the configuration options for RabbitMQ.
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// Gets or sets the hostname of the RabbitMQ server.
    /// This property is optional and can be an empty string.
    /// </summary>
    public string Hostname { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the port number of the RabbitMQ server.
    /// This property is optional and defaults to 0 if not set.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Gets or sets the username used to connect to the RabbitMQ server.
    /// This property is optional and can be an empty string.
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the password used to connect to the RabbitMQ server.
    /// This property is optional and can be an empty string.
    /// </summary>
    public string Password { get; init; } = string.Empty;
}