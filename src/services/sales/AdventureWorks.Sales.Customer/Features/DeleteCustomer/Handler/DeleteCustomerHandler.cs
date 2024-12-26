using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;
using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Handler;

/// <summary>
/// Handles the deletion of a customer.
/// Implements <see cref="IRequestHandler{DeleteCustomerRequest, DeleteCustomerResponse}"/>.
/// </summary>
public class DeleteCustomerHandler(IUnitOfWork unitOfWork, 
                                   IDistributedCache cache, 
                                   ILogger<DeleteCustomerHandler> logger) : 
             BaseHandler<DeleteCustomerHandler>(unitOfWork, 
                                                cache, 
                                                logger), IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
{
    /// <summary>
    /// Handles the deletion of a customer based on the provided request.
    /// </summary>
    /// <param name="request">The request containing the ID of the customer to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="DeleteCustomerResponse"/> indicating the result of the deletion.</returns>
    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request,
                                                     CancellationToken cancellationToken = default)
    {
        Customer? customer = await UnitOfWork.Repository<Customer>().GetByIdAsync(request.Id);

        if (customer is null)
            return new NotFoundCustomerResponse();

        UnitOfWork.Repository<Customer>().Delete(customer);
        int result = await UnitOfWork.CommitAsync();
        
        if (result > 0)
            return new DeleteCustomerResponse();
        return new BadRequestCustomerResponse();
    }
}