namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

public class ConflictCustomerResponse : PostCustomerResponse
{
    public ConflictCustomerResponse() : base(HttpStatusCode.Conflict, "Resource already exists")
    {
    }
}