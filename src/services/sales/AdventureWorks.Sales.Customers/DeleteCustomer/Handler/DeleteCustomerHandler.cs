using AdventureWorks.Sales.Customers.DeleteCustomer.Request;
using AdventureWorks.Sales.Customers.DeleteCustomer.Response;

namespace AdventureWorks.Sales.Customers.DeleteCustomer.Handler;

public class DeleteCustomerHandler : BaseHandler<DeleteCustomerHandler>, IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
{
    public DeleteCustomerHandler(IUnitOfWork unitOfWork,
                                 IDistributedCache cache,
                                 ILogger<DeleteCustomerHandler> logger)
                          : base(unitOfWork,
                                 cache,
                                 logger)
    {
    }

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