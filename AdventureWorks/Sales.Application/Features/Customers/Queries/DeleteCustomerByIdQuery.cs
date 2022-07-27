namespace Sales.Application.Features.Customers.Queries;

public class DeleteCustomerByIdQuery : IRequest<BaseResponse<object>>
{
    internal int Id { get; }

    public DeleteCustomerByIdQuery(int id) => Id = id;
}