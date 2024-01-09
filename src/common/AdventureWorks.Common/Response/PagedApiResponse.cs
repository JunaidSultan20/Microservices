namespace AdventureWorks.Common.Response;

public class PagedApiResponse<TEntity> : ApiResponse<TEntity>
{
    [JsonPropertyOrder(5)]
    [JsonProperty(propertyName: "paginationData")]
    public PaginationData? PaginationData { get; set; }

    public PagedApiResponse() { }

    public PagedApiResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message) { }

    public PagedApiResponse(HttpStatusCode statusCode, string? message, TEntity? result, PaginationData? paginationData)
                            : base(statusCode, message, result)
                            => PaginationData = paginationData;
}