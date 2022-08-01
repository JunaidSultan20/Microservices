namespace Sales.Application.Features.Customers.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, BaseResponse<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CustomerDto>> Handle(UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        Customer? customer = await _unitOfWork.ICustomerRepository.GetByIdAsync(request.Id);
        if (customer == null)
            return new BaseResponse<CustomerDto>(HttpStatusCode.NotFound, $"No customer found against id: {request.Id}");
        _mapper.Map<UpdateCustomerDto, Customer>(request.Customer, customer);
        await _unitOfWork.ICustomerRepository.UpdateAsync(customer);
        int result = await _unitOfWork.Commit();
        if (result > 0)
            return new BaseResponse<CustomerDto>(HttpStatusCode.NoContent,
                $"Customer updated with id: {request.Id}", _mapper.Map<CustomerDto>(customer));
        return new BaseResponse<CustomerDto>(HttpStatusCode.BadRequest, $"Unable to update customer with id: {request.Id}");
    }
}