using AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Handler;

/// <summary>
/// Handles requests to retrieve a customer by their ID.
/// Implements <see cref="IRequestHandler{TRequest,TResponse}"/> interface.
/// Initializes a new instance of the <see cref="GetCustomerByIdHandler"/> class.
/// </summary>
/// <param name="unitOfWork">The unit of work for database operations.</param>
/// <param name="cache">The distributed cache for caching operations.</param>
/// <param name="logger">The logger for logging events.</param>
public class GetCustomerByIdHandler(IUnitOfWork unitOfWork,
                                    IDistributedCache cache,
                                    ILogger<GetCustomerByIdHandler> logger) : 
             BaseHandler<GetCustomerByIdHandler>(unitOfWork, 
                                                 cache, 
                                                 logger), IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    /// <summary>
    /// Handles the request to get a customer by ID.
    /// </summary>
    /// <param name="request">The request containing the customer ID.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response for the customer retrieval.</returns>
    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request,
                                                      CancellationToken cancellationToken = default)
    {
        Customer? customer = await UnitOfWork.Repository<Customer>().GetByIdAsync(request.Id);

        if (customer is null)
            return new NotFoundCustomerByIdResponse();

        return new GetCustomerByIdResponse(customer.Adapt<CustomerDto>());
    }
}