namespace Sales.Application.Features.Customers.Queries;

public class GetAllCustomersQuery : IRequest<PaginationResponse<IEnumerable<CustomerWithLinksDto>>>
{
    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }

    public string? Fields { get; set; }

    public GetAllCustomersQuery(PaginationParameters paginationParameters)
    {
        PageNumber = paginationParameters.PageNumber;
        PageSize = paginationParameters.PageSize;
        Fields = paginationParameters.Fields;
    }
}