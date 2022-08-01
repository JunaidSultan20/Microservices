namespace AdventureWorks.Common.Response;

public class ApiException
{
    internal int? ExceptionId { get; set; }

    internal string? ExceptionMessage { get; set; }

    internal string? Details { get; set; }

    internal string? InnerException { get; set; }

    internal string? ReferenceDocumentLink { get; set; }

    internal IEnumerable<ValidationError>? ValidationErrors { get; set; }

    public ApiException() { }

    public ApiException(int? exceptionId, string message, string details, string innerException, string? referenceDocumentLink)
    {
        ExceptionId = exceptionId;
        ExceptionMessage = message;
        Details = details;
        InnerException = innerException;
        if (!string.IsNullOrEmpty(referenceDocumentLink))
            ReferenceDocumentLink = referenceDocumentLink;
    }

    public ApiException(int? exceptionId, string message, string details, string innerException, string link, IEnumerable<ValidationError> validationErrors)
        : this(exceptionId, message, details, innerException, link)
    {
        ValidationErrors = validationErrors;
    }
}