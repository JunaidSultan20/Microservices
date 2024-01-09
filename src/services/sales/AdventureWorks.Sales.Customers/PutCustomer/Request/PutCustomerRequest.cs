using AdventureWorks.Sales.Customers.PutCustomer.Response;

namespace AdventureWorks.Sales.Customers.PutCustomer.Request;

public class PutCustomerRequest : IRequest<PutCustomerResponse>
{
    internal UpdateCustomerDto Customer { get; set; }

    public PutCustomerRequest(UpdateCustomerDto customer) => Customer = customer;
}