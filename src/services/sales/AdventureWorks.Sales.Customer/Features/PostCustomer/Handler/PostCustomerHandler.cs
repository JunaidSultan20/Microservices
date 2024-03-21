using AdventureWorks.Sales.Customers.Features.PostCustomer.Request;
using AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Handler;

public class PostCustomerHandler(IUnitOfWork unitOfWork, 
                                 IDistributedCache cache, 
                                 ILogger<PostCustomerHandler> logger) : 
             BaseHandler<PostCustomerHandler>(unitOfWork, 
                                              cache, 
                                              logger), IRequestHandler<PostCustomerRequest, PostCustomerResponse>
{
    public async Task<PostCustomerResponse> Handle(PostCustomerRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        Customer customer = await UnitOfWork.Repository<Customer>()
                                            .AddAsync(request.Customer.Adapt<Customer>(), cancellationToken);

        int result = await UnitOfWork.CommitAsync();

        if (result > 0)
            return new PostCustomerResponse(customer.Adapt<CustomerDto>());

        return new PostCustomerResponse(HttpStatusCode.BadRequest, Messages.UnableToCreateCustomer);
    }
}