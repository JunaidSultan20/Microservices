using Sales.Application.DTOs.Customer;

namespace Sales.Application.Features.Customers.Queries;

public class GetCustomerByIdQuery : IRequest<BaseResponse<CustomerDto>>
{
    internal int? Id { get; }

    public GetCustomerByIdQuery(int? id) => Id = id;
}