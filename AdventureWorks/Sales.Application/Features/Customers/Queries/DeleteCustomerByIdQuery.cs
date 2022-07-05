namespace Sales.Application.Features.Customers.Queries;

public class DeleteCustomerByIdQuery : IRequest<BaseResponse<object>>
{
    public int Id { get; set; }

    public DeleteCustomerByIdQuery(int id)
    {
        Id = id;
    }
}