namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

public class PutCustomerResponse(HttpStatusCode statusCode, string message)
    : ApiResponse<CustomerDto>(statusCode, message)
{
    public PutCustomerResponse(CustomerDto result) : this(HttpStatusCode.OK, $"Record updated for customer: {result.CustomerId}") => Result = result;
}