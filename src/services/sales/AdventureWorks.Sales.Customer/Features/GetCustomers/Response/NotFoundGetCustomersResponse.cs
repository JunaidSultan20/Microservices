namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

public class NotFoundGetCustomersResponse()
    : GetCustomersResponse(HttpStatusCode.NotFound, "No customer record exists in database.");