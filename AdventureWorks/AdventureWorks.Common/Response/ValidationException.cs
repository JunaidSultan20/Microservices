using System.Text.Json.Serialization;

namespace AdventureWorks.Common.Response;

public class ValidationException : ResponseEntity
{
    /// <summary>
    /// Validation error list
    /// </summary>
    [JsonPropertyOrder(3)]
    internal IReadOnlyList<ValidationError>? Errors { get; }

    public ValidationException()
        : base(HttpStatusCode.UnprocessableEntity, "One or more validation error occurred", false)
    { }

    public ValidationException(IReadOnlyList<ValidationError> errors)
        : this()
    {
        Errors = errors;
    }
}