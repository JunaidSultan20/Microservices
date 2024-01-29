namespace AdventureWorks.Common.Response;

public class ApiResult
{
    /// <summary>
    /// Returns the status code of the request
    /// </summary>
    [JsonProperty(PropertyName = "statusCode", Order = 1)]
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Message description
    /// </summary>
    [JsonProperty(PropertyName = "message", Order = 2)]
    public string? Message { get; set; }

    /// <summary>
    /// Success/Fail indicator
    /// </summary>
    [JsonProperty(PropertyName = "isSuccessful", Order = 3)]
    public bool? IsSuccessful { get; set; }

    public ApiResult() { }

    /// <summary>
    /// Acts as a base method for response fields
    /// </summary>
    /// <param name="statusCode" example="200"></param>
    /// <param name="message" example="Records retrieved successfully."></param>
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
            HttpStatusCode.OK => "Records retrieved successfully.",
            HttpStatusCode.Created => "Record added successfully.",
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