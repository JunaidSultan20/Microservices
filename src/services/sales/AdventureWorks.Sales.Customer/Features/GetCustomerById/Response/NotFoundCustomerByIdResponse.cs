namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

public class NotFoundCustomerByIdResponse : GetCustomerByIdResponse
{
    public NotFoundCustomerByIdResponse() : base(HttpStatusCode.NotFound, Messages.NotFoundById)
    {
    }
}