namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

public class NotFoundGetCustomersResponse : GetCustomersResponse
{
    public NotFoundGetCustomersResponse() : base(HttpStatusCode.NotFound, "No customer record exists in database.")
    {
    }
}