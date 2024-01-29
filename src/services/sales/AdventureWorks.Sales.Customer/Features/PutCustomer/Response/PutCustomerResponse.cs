namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

public class PutCustomerResponse : ApiResponse<CustomerDto>
{
    public PutCustomerResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }

    public PutCustomerResponse(CustomerDto result) : this(HttpStatusCode.OK, $"Record updated for customer: {result.CustomerId}") => Result = result;
}