using Sales.Application.Features.Customers.Commands;

namespace Sales.Application.Features.Customers.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, BaseResponse<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CustomerDto>> Handle(CreateCustomerCommand request,
        CancellationToken cancellationToken = default)
    {
        Customer customer = await _unitOfWork.ICustomerRepository.AddAsync(_mapper.Map<CreateCustomerDto, Customer>(request.Customer));
        int result = await _unitOfWork.Commit();
        if (result > 0)
            return new BaseResponse<CustomerDto>(HttpStatusCode.Created,
                $"Customer created with id {customer.CustomerId}", _mapper.Map<CustomerDto>(customer));
        return new BaseResponse<CustomerDto>(HttpStatusCode.BadRequest, "Unable to create new customer");
    }
}