using AdventureWorks.Sales.Customers.PostCustomer.Request;
using AdventureWorks.Sales.Customers.PostCustomer.Response;

namespace AdventureWorks.Sales.Customers.PostCustomer.Handler;

public class PostCustomerHandler : BaseHandler<PostCustomerHandler>, IRequestHandler<PostCustomerRequest, PostCustomerResponse>
{
    public PostCustomerHandler(IUnitOfWork unitOfWork, IDistributedCache cache, ILogger<PostCustomerHandler> logger) : base(unitOfWork, cache, logger)
    {
    }

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