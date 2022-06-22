namespace AdventureWorks.Common.Response;

public class ApiException
{
    public int? ExceptionId { get; set; }

    public string ExceptionMessage { get; set; }

    public string Details { get; set; }

    public string InnerException { get; set; }

    public string ReferenceDocumentLink { get; set; }

    public IEnumerable<ValidationError> ValidationErrors { get; set; }

    public ApiException(int? exceptionId, string message, string details, string innerException, string link)
    {
        ExceptionId = exceptionId;
        ExceptionMessage = message;
        Details = details;
        InnerException = innerException;
        if (!string.IsNullOrEmpty(link))
            ReferenceDocumentLink = link;
    }

    public ApiException(int? exceptionId, string message, string details, string innerException, string link, IEnumerable<ValidationError> validationErrors)
        : this(exceptionId, message, details, innerException, link)
    {
        ValidationErrors = validationErrors;
    }
}