namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

/// <summary>
/// Represents the response for getting a customer by their ID.
/// Inherits from <see cref="ApiResponse{CustomerDto}"/>.
/// </summary>
/// <param name="statusCode">The HTTP status code for the response.</param>
/// <param name="message">The message associated with the response.</param>
public class GetCustomerByIdResponse(HttpStatusCode statusCode, string message) : ApiResponse<CustomerDto>(statusCode, message)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerByIdResponse"/> class with the specified customer.
    /// </summary>
    /// <param name="result">The <see cref="CustomerDto"/> representing the customer found.</param>
    public GetCustomerByIdResponse(CustomerDto result) : this(HttpStatusCode.OK, 
                                                              $"Customer with id: {result.CustomerId} found successfully") 
        => Result = result;
}