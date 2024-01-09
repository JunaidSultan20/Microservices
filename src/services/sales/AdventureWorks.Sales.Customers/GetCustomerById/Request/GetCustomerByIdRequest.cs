using AdventureWorks.Sales.Customers.GetCustomerById.Response;

namespace AdventureWorks.Sales.Customers.GetCustomerById.Request;

public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
{
    public int Id { get; set; }

    public GetCustomerByIdRequest(int id) => Id = id;
}