namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents a response with a result of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the result data.</typeparam>
public class ApiResponse<TEntity> : ApiResult
{
    /// <summary>
    /// Gets or sets the result data of type <typeparamref name="TEntity"/>.
    /// </summary>
    [JsonProperty(PropertyName = "result", Order = 4)]
    public TEntity? Result { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{TEntity}"/> class.
    /// </summary>
    public ApiResponse() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{TEntity}"/> class with a specified status code and message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code. Example: <c>200</c>.</param>
    /// <param name="message">The response message. Example: <c>Records retrieved successfully.</c>.</param>
    protected ApiResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{TEntity}"/> class with a specified status code, message, and result.
    /// </summary>
    /// <param name="statusCode">The HTTP status code. Example: <c>200</c>.</param>
    /// <param name="message">The response message. Example: <c>Records retrieved successfully.</c>.</param>
    /// <param name="result">The result data of type <typeparamref name="TEntity"/>.</param>
    protected ApiResponse(HttpStatusCode statusCode, string? message, TEntity? result) : this(statusCode, message) => Result = result;
}