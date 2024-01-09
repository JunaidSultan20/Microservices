namespace AdventureWorks.Common.Response;

/// <summary>
/// Generic ApiResponse
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class ApiResponse<TEntity> : ApiResult
{
    /// <summary>
    /// TEntity of result type
    /// </summary>
    [JsonProperty(PropertyName = "result", Order = 4)]
    public TEntity? Result { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    protected ApiResponse() { }

    /// <summary>
    /// Constructor with status code and message
    /// </summary>
    /// <param name="statusCode" example="200"></param>
    /// <param name="message" example="Records retrieved successfully."></param>
    protected ApiResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message) { }

    /// <summary>
    /// Constructor with status code, message and result parameters
    /// </summary>
    /// <param name="statusCode" example="200"></param>
    /// <param name="message" example="Records retrieved successfully."></param>
    /// <param name="result"></param>
    protected ApiResponse(HttpStatusCode statusCode, string? message, TEntity? result)
                        : this(statusCode, message)
                        => Result = result;
}
