namespace AdventureWorks.Sales.Customers.DeleteCustomer.Response;

public class NotFoundCustomerResponse : DeleteCustomerResponse
{
    public NotFoundCustomerResponse() : base(HttpStatusCode.NotFound, Messages.NotFoundById)
    {
    }
}