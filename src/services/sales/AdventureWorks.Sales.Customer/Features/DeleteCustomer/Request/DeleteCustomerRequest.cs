using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;

public class DeleteCustomerRequest(int id) : IRequest<DeleteCustomerResponse>
{
    internal int Id { get; set; } = id;
}