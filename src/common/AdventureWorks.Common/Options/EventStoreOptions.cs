namespace AdventureWorks.Common.Options;

/// <summary>
/// Represents the configuration options for connecting to an Event Store database.
/// </summary>
public class EventStoreOptions
{
    /// <summary>
    /// Gets or sets the URI of the Event Store server.
    /// This property is required and cannot be an empty string.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "ServerUri is required and cannot be empty.")]
    public string ServerUri { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the database in the Event Store.
    /// This property is required and cannot be an empty string.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Database is required and cannot be empty.")]
    public string Database { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the collection within the database.
    /// This property is optional and can be an empty string.
    /// </summary>
    public string Collection { get; set; } = string.Empty;
}