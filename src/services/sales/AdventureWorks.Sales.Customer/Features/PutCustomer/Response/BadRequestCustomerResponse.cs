namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

public class BadRequestCustomerResponse : PutCustomerResponse
{
    public BadRequestCustomerResponse(string message) : base(HttpStatusCode.BadRequest, message)
    {
    }
}