namespace AdventureWorks.Common.Exceptions;

public class ApiException
{
    internal Guid? Id { get; set; }

    internal string? Message { get; set; }

    internal string? Details { get; set; }

    internal string? InnerException { get; set; }

    internal string? StackTrace { get; set; }

    /// <summary>
    /// Parameter-less constructor
    /// </summary>
    public ApiException() { }

    /// <summary>
    /// Constructor for creating new api exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="details"></param>
    /// <param name="innerException"></param>
    /// <param name="stackTrace"></param>
    public ApiException(string message, string? details, string? innerException, string? stackTrace)
    {
        Id = Guid.NewGuid();
        Message = message;
        Details = details;
        InnerException = innerException;
        StackTrace = stackTrace;
    }

    /// <summary>
    /// Overloaded constructor to create new api exception
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    /// <param name="details"></param>
    /// <param name="innerException"></param>
    /// <param name="stackTrace"></param>
    public ApiException(Guid? id,
                        string message,
                        string? details,
                        string? innerException,
                        string? stackTrace) :
                        this(message,
                            details,
                            innerException,
                            stackTrace) => Id = id;
}