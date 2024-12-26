namespace AdventureWorks.Common.Exceptions;

/// <summary>
/// Represents an exception that occurs during validation of an API request.
/// </summary>
public class ValidationException : ApiResult
{
    /// <summary>
    /// Gets or sets the list of validation errors.
    /// </summary>
    [JsonProperty(PropertyName = "errors", Order = 3)]
    public IReadOnlyList<ValidationError>? Errors { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class 
    /// with the default status code of UnprocessableEntity and a validation error message.
    /// </summary>
    private ValidationException() : base(statusCode: HttpStatusCode.UnprocessableEntity, message: Messages.ValidationError)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified list of validation errors.
    /// </summary>
    /// <param name="errors">The list of validation errors.</param>
    public ValidationException(IReadOnlyList<ValidationError> errors) : this() => Errors = errors;
}