namespace AdventureWorks.Sales.Customers.DeleteCustomer.Response;

public class BadRequestCustomerResponse : DeleteCustomerResponse
{
    public BadRequestCustomerResponse() : base(HttpStatusCode.BadRequest, Messages.UnableToDeleteRecord)
    {
    }
}