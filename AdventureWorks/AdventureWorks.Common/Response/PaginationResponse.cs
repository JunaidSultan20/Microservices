namespace AdventureWorks.Common.Response;

public class PaginationResponse<TEntity> : BaseResponse<TEntity>
{
    public PaginationData? PaginationData { get; set; }

    public PaginationResponse() { }

    public PaginationResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message) { }

    public PaginationResponse(HttpStatusCode statusCode, string? message, TEntity? result, PaginationData? paginationData) :
        base(statusCode, message, result)
    {
        PaginationData = paginationData;
    }

    public PaginationResponse(HttpStatusCode statusCode, string? message, List<string> errors) : base(statusCode, message, errors) { }
}