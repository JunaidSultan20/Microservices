namespace AdventureWorks.Common.Response;

public class ResponseEntity
{
    /// <summary>
    /// Returns the status code of the request
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Message description
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Success/Fail indicator
    /// </summary>
    public bool? IsSuccessful { get; set; }

    public ResponseEntity() { }

    public ResponseEntity(HttpStatusCode statusCode, string message, bool? isSuccessful)
    {
        StatusCode = statusCode;
        Message = message;
        IsSuccessful = isSuccessful;
    }
}