using AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Request;

public class PostCustomerRequest : IRequest<PostCustomerResponse>
{
    public CreateCustomerDto Customer { get; set; }

    public PostCustomerRequest(CreateCustomerDto customer) => Customer = customer;
}