namespace Sales.Application.Features.Customers.Queries;

public class GetCustomerByIdQuery : IRequest<BaseResponse<CustomerDto>>
{
    public int? Id { get; set; }

    public GetCustomerByIdQuery(int? id)
    {
        Id = id;
    }
}