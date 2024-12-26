namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents an error related to validation of a specific field.
/// </summary>
public record ValidationError
{
    /// <summary>
    /// Gets or sets the name of the field that caused the validation error.
    /// </summary>
    /// <remarks>
    /// This property is optional and can be null.
    /// </remarks>
    [JsonProperty(PropertyName = "field", NullValueHandling = NullValueHandling.Ignore)]
    public string? Field { get; set; }

    /// <summary>
    /// Gets the message describing the validation error.
    /// </summary>
    [JsonProperty(PropertyName = "message")]
    public string? Message { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> record with default values.
    /// </summary>
    public ValidationError() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> record with the specified field name and error message.
    /// </summary>
    /// <param name="field">The name of the field that caused the validation error.</param>
    /// <param name="message">The message describing the validation error.</param>
    public ValidationError(string? field, string? message) => (Field, Message) = (field ?? null, message ?? null);
}