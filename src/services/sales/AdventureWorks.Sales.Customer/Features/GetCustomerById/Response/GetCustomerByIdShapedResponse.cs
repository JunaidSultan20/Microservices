namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

public class GetCustomerByIdShapedResponse : ApiResponse<ExpandoObject>
{
    public GetCustomerByIdShapedResponse(string? message, ExpandoObject result) : base(HttpStatusCode.OK, message, result)
    {
    }
}