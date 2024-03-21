using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;
using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Handler;

public class DeleteCustomerHandler(IUnitOfWork unitOfWork, 
                                   IDistributedCache cache, 
                                   ILogger<DeleteCustomerHandler> logger) : 
             BaseHandler<DeleteCustomerHandler>(unitOfWork, 
                                                cache, 
                                                logger), IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
{
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