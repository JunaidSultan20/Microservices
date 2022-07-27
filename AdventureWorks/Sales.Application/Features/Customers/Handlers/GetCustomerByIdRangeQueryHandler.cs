using Sales.Application.DTOs.Customer;

namespace Sales.Application.Features.Customers.Handlers;

public class GetCustomerByIdRangeQueryHandler : IRequestHandler<GetCustomersByIdRangeQuery, BaseResponse<IEnumerable<CustomerWithLinksDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerByIdRangeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<CustomerWithLinksDto>>> Handle(GetCustomersByIdRangeQuery request,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Customer> result = await _unitOfWork.ICustomerRepository.GetAsync(customer =>
            customer.CustomerId >= request.MinCustomerId && customer.CustomerId <= request.MaxCustomerId);
        if (!result.Any())
            return new BaseResponse<IEnumerable<CustomerWithLinksDto>>(HttpStatusCode.NotFound,
                "Unable to find customers against specified range.");
        return new BaseResponse<IEnumerable<CustomerWithLinksDto>>(HttpStatusCode.OK, "",
            _mapper.Map<IEnumerable<CustomerWithLinksDto>>(result));
    }
}