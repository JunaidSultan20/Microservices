namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

public class BadRequestCustomerResponse : DeleteCustomerResponse
{
    public BadRequestCustomerResponse() : base(HttpStatusCode.BadRequest, Messages.UnableToDeleteRecord)
    {
    }
}