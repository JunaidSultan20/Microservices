namespace AdventureWorks.Sales.Customers.GetCustomerById.Response;

public class NotFoundCustomerByIdResponse : GetCustomerByIdResponse
{
    public NotFoundCustomerByIdResponse() : base(HttpStatusCode.NotFound, Messages.NotFoundById)
    {
    }
}