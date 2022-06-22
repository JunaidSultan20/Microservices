namespace Sales.Application.Features.Customers.Handlers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CustomerDto>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Customer result = await _unitOfWork.ICustomerRepository.GetByIdAsync(request.Id);
        if (result != null)
            return new BaseResponse<CustomerDto>(HttpStatusCode.OK, null, _mapper.Map<CustomerDto>(result));
        return new BaseResponse<CustomerDto>(HttpStatusCode.NotFound, null, null);
    }
}