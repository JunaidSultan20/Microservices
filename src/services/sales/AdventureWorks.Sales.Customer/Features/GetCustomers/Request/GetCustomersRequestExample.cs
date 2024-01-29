namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Request;

public class GetCustomersRequestExample : IExamplesProvider<GetCustomersRequest>
{
    public GetCustomersRequest GetExamples()
    {
        PaginationParameters parameters = new PaginationParameters(pageNumber: 1, pageSize: 5, fields: null);

        return new GetCustomersRequest(parameters);
    }
}