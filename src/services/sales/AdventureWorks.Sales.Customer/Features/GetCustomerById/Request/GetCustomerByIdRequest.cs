using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;

public class GetCustomerByIdRequest(int id) : IRequest<GetCustomerByIdResponse>
{
    public int Id { get; set; } = id;
}