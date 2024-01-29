using AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Request;

public class PutCustomerRequest : IRequest<PutCustomerResponse>
{
    internal UpdateCustomerDto Customer { get; set; }

    public PutCustomerRequest(UpdateCustomerDto customer) => Customer = customer;
}