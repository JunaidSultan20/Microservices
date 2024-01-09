namespace AdventureWorks.Sales.Customers.PostCustomer.Response;

public class ConflictCustomerResponse : PostCustomerResponse
{
    public ConflictCustomerResponse() : base(HttpStatusCode.Conflict, "Resource already exists")
    {
    }
}