namespace Sales.Application.Features.Customers.Handlers;

public class DeleteCustomerByIdQueryHandler : IRequestHandler<DeleteCustomerByIdQuery, BaseResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<object>> Handle(DeleteCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = await _unitOfWork.ICustomerRepository.GetByIdAsync(request.Id);
        if (customer == null)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, $"No customer found against id: {request.Id}.",
                request.Id);
        await _unitOfWork.ICustomerRepository.DeleteAsync(customer);
        return new BaseResponse<object>(HttpStatusCode.NoContent, "Customer deleted successfully.", request.Id);
    }
}