namespace AdventureWorks.Sales.Customers.GetCustomers.Response;

public class GetCustomersResponse : PagedApiResponse<IReadOnlyList<CustomerDto>>
{
    [JsonConstructor]
    public GetCustomersResponse()
    {
    }

    public GetCustomersResponse(HttpStatusCode statusCode, string? message)
                         : base(statusCode, message)
    {
    }

    public GetCustomersResponse(IReadOnlyList<CustomerDto>? result, PaginationData pagination)
                         : this(HttpStatusCode.OK, Messages.RecordsRetrievedSuccessfully)
                            => (Result, PaginationData) = (result, pagination);
}