using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Request;

public class GetCustomersRequest : IRequest<GetCustomersResponse>
{
    internal PaginationParameters Pagination { get; set; }

    public GetCustomersRequest(PaginationParameters pagination) => Pagination = pagination;
}