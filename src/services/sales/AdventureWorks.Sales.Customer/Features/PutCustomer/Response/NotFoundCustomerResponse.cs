namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

public class NotFoundCustomerResponse : PutCustomerResponse
{
    public NotFoundCustomerResponse() : base(HttpStatusCode.NotFound, Messages.NotFoundById)
    {
    }
}