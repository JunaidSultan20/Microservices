namespace AdventureWorks.Common.Options;

/// <summary>
/// Represents the configuration options for request logging.
/// </summary>
public class RequestLogOptions
{
    /// <summary>
    /// Gets or sets the URI of the server where request logs are stored.
    /// This property is required and cannot be empty.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string ServerUri { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the database where request logs are stored.
    /// This property is required and cannot be empty.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Database { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the collection where request logs are stored.
    /// This property is required and cannot be empty.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Collection { get; init; } = string.Empty;
}