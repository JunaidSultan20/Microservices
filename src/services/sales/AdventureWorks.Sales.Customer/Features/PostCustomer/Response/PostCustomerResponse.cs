namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

public class PostCustomerResponse(HttpStatusCode statusCode, string message) : ApiResponse<CustomerDto>(statusCode, message)
{
    public PostCustomerResponse(CustomerDto result) : this(HttpStatusCode.Created,
                                                           $"Customer created with id: {result.CustomerId}") => Result = result;
}