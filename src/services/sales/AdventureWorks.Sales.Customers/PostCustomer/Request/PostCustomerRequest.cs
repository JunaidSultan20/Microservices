using AdventureWorks.Sales.Customers.PostCustomer.Response;

namespace AdventureWorks.Sales.Customers.PostCustomer.Request;

public class PostCustomerRequest : IRequest<PostCustomerResponse>
{
    public CreateCustomerDto Customer { get; set; }

    public PostCustomerRequest(CreateCustomerDto customer) => Customer = customer;
}