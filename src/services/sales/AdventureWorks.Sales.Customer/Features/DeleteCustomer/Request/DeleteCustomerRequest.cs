using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;

/// <summary>
/// Request object for deleting a customer by ID.
/// </summary>
/// <param name="id">The unique identifier of the customer to be deleted.</param>
public class DeleteCustomerRequest(int id) : IRequest<DeleteCustomerResponse>
{
    /// <summary>
    /// Gets or sets the customer ID to be deleted.
    /// </summary>
    internal int Id { get; set; } = id;
}