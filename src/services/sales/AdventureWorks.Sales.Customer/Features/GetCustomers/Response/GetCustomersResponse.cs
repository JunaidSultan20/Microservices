namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

public class GetCustomersResponse : PagedApiResponse<List<CustomerDto>>
{
    [JsonConstructor]
    public GetCustomersResponse()
    {
    }

    public GetCustomersResponse(HttpStatusCode statusCode, string? message)
        : base(statusCode, message)
    {
    }

    public GetCustomersResponse(List<CustomerDto>? result, PaginationData pagination)
        : this(HttpStatusCode.OK, Messages.RecordsRetrievedSuccessfully)
        => (Result, PaginationData) = (result, pagination);
}