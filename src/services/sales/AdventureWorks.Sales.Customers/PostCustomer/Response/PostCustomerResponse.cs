namespace AdventureWorks.Sales.Customers.PostCustomer.Response;

public class PostCustomerResponse : ApiResponse<CustomerDto>
{
    public PostCustomerResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }

    public PostCustomerResponse(CustomerDto result) : this(HttpStatusCode.Created,
                                                           $"Customer created with id: {result.CustomerId}")
    {
    }
}