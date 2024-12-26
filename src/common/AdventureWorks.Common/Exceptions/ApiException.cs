namespace AdventureWorks.Common.Exceptions;

/// <summary>
/// Represents a custom exception for API errors, including message, details, and stack trace.
/// </summary>
public class ApiException
{
    /// <summary>
    /// Gets or sets the unique identifier for the exception.
    /// </summary>
    [JsonProperty(PropertyName = "id", Order = 1)]
    internal Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets the message describing the exception.
    /// </summary>
    [JsonProperty(PropertyName = "message", Order = 2)]
    internal string? Message { get; set; }

    /// <summary>
    /// Gets or sets additional details about the exception.
    /// </summary>
    [JsonProperty(PropertyName = "details", Order = 3)]
    internal string? Details { get; set; }

    /// <summary>
    /// Gets or sets the message from an inner exception, if present.
    /// </summary>
    [JsonProperty(PropertyName = "innerException", Order = 4)]
    internal string? InnerException { get; set; }

    /// <summary>
    /// Gets or sets the stack trace for the exception.
    /// </summary>
    [JsonProperty(PropertyName = "stackTrace", Order = 5)]
    internal string? StackTrace { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    protected ApiException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with the specified message.
    /// </summary>
    /// <param name="message">The message describing the exception.</param>
    public ApiException(string message) => (Id, Message) = (Id, message);

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with the specified message, details, inner exception, and stack trace.
    /// </summary>
    /// <param name="message">The message describing the exception.</param>
    /// <param name="details">Additional details about the exception.</param>
    /// <param name="innerException">The inner exception message, if present.</param>
    /// <param name="stackTrace">The stack trace of the exception.</param>
    public ApiException(string message, string? details, string? innerException, string? stackTrace) : this(message)
    {
        Details = details;
        InnerException = innerException;
        StackTrace = stackTrace;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with the specified id, message, details, inner exception, and stack trace.
    /// </summary>
    /// <param name="id">The unique identifier for the exception.</param>
    /// <param name="message">The message describing the exception.</param>
    /// <param name="details">Additional details about the exception.</param>
    /// <param name="innerException">The inner exception message, if present.</param>
    /// <param name="stackTrace">The stack trace of the exception.</param>
    public ApiException(Guid? id, string message, string? details, string? innerException, string? stackTrace) :
                   this(message, details, innerException, stackTrace) => Id = id;
}