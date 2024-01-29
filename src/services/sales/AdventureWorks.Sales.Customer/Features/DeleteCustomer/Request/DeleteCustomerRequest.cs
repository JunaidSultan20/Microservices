using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;

public class DeleteCustomerRequest : IRequest<DeleteCustomerResponse>
{
    internal int Id { get; set; }

    public DeleteCustomerRequest(int id) => Id = id;
}