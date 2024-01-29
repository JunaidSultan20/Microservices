using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;

public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
{
    public int Id { get; set; }

    public GetCustomerByIdRequest(int id) => Id = id;
}