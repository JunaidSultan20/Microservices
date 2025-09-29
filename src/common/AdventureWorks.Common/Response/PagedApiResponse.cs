namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents a paginated API response that includes the result data and pagination information.
/// </summary>
/// <typeparam name="TEntity">The type of the result data.</typeparam>
public class PagedApiResponse<TEntity> : ApiResponse<TEntity>
{
    /// <summary>
    /// Gets or sets the pagination data for the response.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///     "currentPage": 1,
    ///     "totalPages": 10,
    ///     "pageSize": 20,
    ///     "totalItems": 200
    /// }
    /// </code>
    /// </example>
    [JsonProperty(PropertyName = "paginationData", Order = 5)]
    public PaginationData? PaginationData { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedApiResponse{TEntity}"/> class.
    /// </summary>
    public PagedApiResponse() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedApiResponse{TEntity}"/> class with specified status code and message.
    /// </summary>
    /// <param name="statusCode">The status code of the response.</param>
    /// <param name="message">The message describing the response.</param>
    /// <example>
    /// <code>
    /// "PagedApiResponse response = new PagedApiResponse(HttpStatusCode.OK, "Request successful.");"
    /// </code>
    /// </example>
    public PagedApiResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedApiResponse{TEntity}"/> class with specified status code, message, result data, and pagination data.
    /// </summary>
    /// <param name="statusCode">The status code of the response.</param>
    /// <param name="message">The message describing the response.</param>
    /// <param name="result">The result data of the response.</param>
    /// <param name="paginationData">The pagination data for the response.</param>
    public PagedApiResponse(HttpStatusCode statusCode, string? message, TEntity? result, PaginationData? paginationData)
        : base(statusCode, message, result)
        => PaginationData = paginationData;
}