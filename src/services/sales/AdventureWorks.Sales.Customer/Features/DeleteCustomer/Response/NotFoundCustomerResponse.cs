namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

public class NotFoundCustomerResponse : DeleteCustomerResponse
{
    public NotFoundCustomerResponse() : base(HttpStatusCode.NotFound, Messages.NotFoundById)
    {
    }
}