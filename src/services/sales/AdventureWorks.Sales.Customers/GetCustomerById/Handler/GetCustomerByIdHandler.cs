using AdventureWorks.Sales.Customers.GetCustomerById.Request;
using AdventureWorks.Sales.Customers.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.GetCustomerById.Handler;

public class GetCustomerByIdHandler : BaseHandler<GetCustomerByIdHandler>, IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    public GetCustomerByIdHandler(IUnitOfWork unitOfWork,
                                  IDistributedCache cache,
                                  ILogger<GetCustomerByIdHandler> logger)
                           : base(unitOfWork,
                                  cache,
                                  logger)
    {
    }

    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request,
                                                      CancellationToken cancellationToken = default)
    {
        Customer? customer = await UnitOfWork.Repository<Customer>().GetByIdAsync(request.Id);

        if (customer is null)
            return new NotFoundCustomerByIdResponse();

        return new GetCustomerByIdResponse(customer.Adapt<CustomerDto>());
    }
}