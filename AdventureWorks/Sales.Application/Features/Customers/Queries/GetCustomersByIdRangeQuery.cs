namespace Sales.Application.Features.Customers.Queries;

public class GetCustomersByIdRangeQuery : IRequest<BaseResponse<IEnumerable<CustomerDto>>>
{
    public int? MinCustomerId { get; set; }
    public int? MaxCustomerId { get; set; }

    public GetCustomersByIdRangeQuery(int minCustomerId, int maxCustomerId)
    {
        MinCustomerId = minCustomerId;
        MaxCustomerId = maxCustomerId;
    }
}