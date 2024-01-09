namespace AdventureWorks.Sales.Customers.GetCustomerById.Response;

public class GetCustomerByIdResponse : ApiResponse<CustomerDto>
{
    public GetCustomerByIdResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }

    public GetCustomerByIdResponse(CustomerDto result)
                            : this(HttpStatusCode.OK, $"Customer with id: {result.CustomerId} found successfully") => Result = result;
}