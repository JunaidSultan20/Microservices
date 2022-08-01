namespace Sales.Application.Features.Customers.Handlers;

public class GetCustomerByIdQueryHandler : BaseHandler, IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerDto>>
{
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IMapper _mapper;

    //public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //{
    //    _unitOfWork = unitOfWork;
    //    _mapper = mapper;
    //}

    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<BaseResponse<CustomerDto>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Customer? result = await UnitOfWork.ICustomerRepository.GetByIdAsync(request.Id);
        if (result != null)
            return new BaseResponse<CustomerDto>(HttpStatusCode.OK, null, Mapper.Map<CustomerDto>(result));
        return new BaseResponse<CustomerDto>(HttpStatusCode.NotFound, $"No record found against id: {request.Id}");
    }
}