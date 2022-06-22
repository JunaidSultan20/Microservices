namespace AdventureWorks.Common.Response;

public class PaginationResponse<TEntity> : BaseResponse<TEntity>
{
    public PaginationData? PaginationData { get; set; }

    public PaginationResponse() : base() { }

    public PaginationResponse(HttpStatusCode statusCode, string? message, TEntity? result, PaginationData? paginationData) :
        base(statusCode, message, result)
    {
        PaginationData = paginationData;
    }
}