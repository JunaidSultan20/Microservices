using AdventureWorks.Common.Parameters;
using AdventureWorks.Sales.Customers.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.GetCustomers.Request;

public class GetCustomersRequest : IRequest<GetCustomersResponse>
{
    internal PaginationParameters Pagination { get; set; }

    public GetCustomersRequest(PaginationParameters pagination) => Pagination = pagination;
}