using Sales.Application.DTOs.Customer;

namespace Sales.Application.Features.Customers.Queries;

public class GetCustomersByIdRangeQuery : IRequest<BaseResponse<IEnumerable<CustomerWithLinksDto>>>
{
    internal int? MinCustomerId { get; }
    internal int? MaxCustomerId { get; }

    public GetCustomersByIdRangeQuery(int minCustomerId, int maxCustomerId) => (MinCustomerId, MaxCustomerId) = (minCustomerId, maxCustomerId);
}