using AdventureWorks.Contracts.EventStreaming;
using AdventureWorks.Sales.Customers.DomainEvents;
using AdventureWorks.Sales.Customers.Features.PostCustomer.Request;
using AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Handler;

public class PostCustomerHandler(IUnitOfWork unitOfWork, 
                                 IDistributedCache cache, 
                                 ILogger<PostCustomerHandler> logger,
                                 CustomerAggregate customerAggregate,
                                 IEventStore eventStore) : 
             BaseHandler<PostCustomerHandler>(unitOfWork, 
                                              cache, 
                                              logger), IRequestHandler<PostCustomerRequest, PostCustomerResponse>
{
    public async Task<PostCustomerResponse> Handle(PostCustomerRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        //Customer customer = await UnitOfWork.Repository<Customer>()
        //                                    .AddAsync(request.Customer.Adapt<Customer>(), cancellationToken);

        //int result = await UnitOfWork.CommitAsync();

        customerAggregate.CustomerCreatedEvent(request.Customer.CustomerId, 
                                               request.Customer.PersonId, 
                                               request.Customer.AccountNumber, 
                                               Guid.Parse(request.Customer.Rowguid), 
                                               request.Customer.ModifiedDate);

        await eventStore.SaveAsync(customerAggregate, request.Customer.ToString(), "customer");

        //if (result > 0)
            return new PostCustomerResponse(request.Adapt<CustomerDto>());

        //return new PostCustomerResponse(HttpStatusCode.BadRequest, Messages.UnableToCreateCustomer);
    }
}