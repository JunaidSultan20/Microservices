using Sales.Application.DTOs.Customer;

namespace Sales.Application.Features.Customers.Handlers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUrlService _urlService;

    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUrlService urlService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _urlService = urlService;
    }

    public async Task<BaseResponse<CustomerDto>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Customer? result = await _unitOfWork.ICustomerRepository.GetByIdAsync(request.Id);
        if (result != null)
            return new BaseResponse<CustomerDto>(HttpStatusCode.OK, null, _mapper.Map<CustomerDto>(result));
        return new BaseResponse<CustomerDto>(HttpStatusCode.NotFound, $"No record found against id: {request.Id}");
    }
}