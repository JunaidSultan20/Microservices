using AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Request;

public class PutCustomerRequest(UpdateCustomerDto customer) : IRequest<PutCustomerResponse>
{
    internal UpdateCustomerDto Customer { get; set; } = customer;
}