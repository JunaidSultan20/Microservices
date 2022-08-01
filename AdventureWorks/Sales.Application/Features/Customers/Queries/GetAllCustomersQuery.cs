namespace Sales.Application.Features.Customers.Queries;

public class GetAllCustomersQuery : IRequest<PaginationResponse<IEnumerable<CustomerWithLinksDto>>>
{
    internal int? PageNumber { get; }

    internal int? PageSize { get; }

    internal string? Fields { get; }

    public GetAllCustomersQuery(PaginationParameters paginationParameters) => (PageNumber, PageSize, Fields) =
        (paginationParameters.PageNumber, paginationParameters.PageSize, paginationParameters.Fields);
}