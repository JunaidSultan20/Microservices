namespace AdventureWorks.Common.Options;

/// <summary>
/// Represents the configuration options for Seq logging.
/// </summary>
public class SeqOptions
{
    /// <summary>
    /// Gets or sets the server URI for Seq.
    /// This property is required and cannot be empty.
    /// </summary>
    [Required]
    public string Server { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the API key for Seq.
    /// This property is required and cannot be empty.
    /// </summary>
    [Required]
    public string ApiKey { get; init; } = string.Empty;
}