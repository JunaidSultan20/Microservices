using AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Handler;

public class GetCustomerByIdHandler(IUnitOfWork unitOfWork,
                                    IDistributedCache cache,
                                    ILogger<GetCustomerByIdHandler> logger) : 
             BaseHandler<GetCustomerByIdHandler>(unitOfWork, 
                                                 cache, 
                                                 logger), IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request,
                                                      CancellationToken cancellationToken = default)
    {
        Customer? customer = await UnitOfWork.Repository<Customer>().GetByIdAsync(request.Id);

        if (customer is null)
            return new NotFoundCustomerByIdResponse();

        return new GetCustomerByIdResponse(customer.Adapt<CustomerDto>());
    }
}