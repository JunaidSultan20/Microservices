using AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Request;

public class PostCustomerRequest(CreateCustomerDto customer) : IRequest<PostCustomerResponse>
{
    public CreateCustomerDto Customer { get; set; } = customer;
}