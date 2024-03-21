using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Request;

public class GetCustomersRequest(PaginationParameters pagination) : IRequest<GetCustomersResponse>
{
    internal PaginationParameters Pagination { get; set; } = pagination;
}