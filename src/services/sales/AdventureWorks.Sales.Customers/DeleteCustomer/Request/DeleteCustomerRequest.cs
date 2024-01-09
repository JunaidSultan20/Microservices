using AdventureWorks.Sales.Customers.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.DeleteCustomer.Request;

public class DeleteCustomerRequest : IRequest<DeleteCustomerResponse>
{
    internal int Id { get; set; }

    public DeleteCustomerRequest(int id) => Id = id;
}