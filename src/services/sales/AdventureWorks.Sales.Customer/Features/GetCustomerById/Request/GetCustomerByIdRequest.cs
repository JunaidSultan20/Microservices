using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;

/// <summary>
/// Represents a request to get a customer by their ID.
/// Implements <see cref="IRequest{GetCustomerByIdResponse}"/>.
/// </summary>
/// <param name="id">The ID of the customer to retrieve.</param>
public class GetCustomerByIdRequest(int id) : IRequest<GetCustomerByIdResponse>
{
    /// <summary>
    /// Gets or sets the ID of the customer.
    /// </summary>
    public int Id { get; set; } = id;
}