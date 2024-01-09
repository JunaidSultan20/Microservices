namespace AdventureWorks.Sales.Customers.PutCustomer.Response;

public class NotFoundCustomerResponse : PutCustomerResponse
{
    public NotFoundCustomerResponse() : base(HttpStatusCode.NotFound, Messages.NotFoundById)
    {
    }
}