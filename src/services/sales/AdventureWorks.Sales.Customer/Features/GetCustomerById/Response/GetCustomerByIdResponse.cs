namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

public class GetCustomerByIdResponse(HttpStatusCode statusCode, string message) : ApiResponse<CustomerDto>(statusCode, message)
{
    public GetCustomerByIdResponse(CustomerDto result) : this(HttpStatusCode.OK, 
                                                              $"Customer with id: {result.CustomerId} found successfully") 
        => Result = result;
}