namespace AdventureWorks.Common.Exceptions;

public class ValidationException : ApiResult
{
    /// <summary>
    /// Validation error list
    /// </summary>
    [JsonProperty(PropertyName = "errors", Order = 3)]
    public IReadOnlyList<ValidationError>? Errors { get; set; }

    private ValidationException() : base(statusCode: HttpStatusCode.UnprocessableEntity,
                                         message: Messages.ValidationError)
    {
    }

    public ValidationException(IReadOnlyList<ValidationError> errors) : this() => Errors = errors;
}