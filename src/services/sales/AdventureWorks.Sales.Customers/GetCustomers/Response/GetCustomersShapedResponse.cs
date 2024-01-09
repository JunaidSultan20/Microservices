namespace AdventureWorks.Sales.Customers.GetCustomers.Response;

public class GetCustomersShapedResponse : PagedApiResponse<IReadOnlyList<ExpandoObject>>
{
    private GetCustomersShapedResponse(HttpStatusCode statusCode, string? message)
                                : base(statusCode, message)
    {
    }

    public GetCustomersShapedResponse(IReadOnlyList<ExpandoObject>? result, PaginationData? pagination)
                               : this(HttpStatusCode.OK, Messages.RecordsRetrievedSuccessfully)
                                  => (Result, PaginationData) = (result, pagination);
}