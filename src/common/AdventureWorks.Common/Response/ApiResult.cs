namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents the result of an API operation, including the status code, message, and success indicator.
/// </summary>
public class ApiResult
{
    /// <summary>
    /// Gets or sets the status code of the request.
    /// </summary>
    [JsonProperty(PropertyName = "statusCode", Order = 1)]
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the message description for the result.
    /// </summary>
    [JsonProperty(PropertyName = "message", Order = 2)]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the request was successful.
    /// </summary>
    [JsonProperty(PropertyName = "isSuccessful", Order = 3)]
    public bool? IsSuccessful { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult"/> class.
    /// </summary>
    public ApiResult() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult"/> class with specified status code and message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code. Example: <c>200</c>.</param>
    /// <param name="message">The message description. Example: <c>Records retrieved successfully.</c>.</param>
    public ApiResult(HttpStatusCode statusCode, string? message)
    {
        StatusCode = statusCode;

        IsSuccessful = statusCode switch
        {
            HttpStatusCode.OK => true,
            HttpStatusCode.Created => true,
            HttpStatusCode.NoContent => true,
            _ => false
        };

        Message = message ?? statusCode switch
        {
            HttpStatusCode.OK => Messages.RecordsRetrievedSuccessfully,
            HttpStatusCode.Created => Messages.RecordAddedSuccessfully,
            HttpStatusCode.NoContent => "Resource deleted successfully.",
            HttpStatusCode.BadRequest => "Invalid resource requested.",
            HttpStatusCode.Unauthorized => "Invalid authentication request.",
            HttpStatusCode.NotFound => "No records found.",
            HttpStatusCode.MethodNotAllowed => "Method not allowed.",
            HttpStatusCode.Conflict => "Request conflict.",
            HttpStatusCode.UnsupportedMediaType => "Media type not supported.",
            HttpStatusCode.InternalServerError => "Internal server error occurred.",
            HttpStatusCode.NotAcceptable => "Request body not acceptable.",
            _ => null
        };
    }
}