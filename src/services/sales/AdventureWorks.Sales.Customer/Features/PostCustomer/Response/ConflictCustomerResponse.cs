namespace AdventureWorks.Sales.Customers.Features.PostCustomer.Response;

public class ConflictCustomerResponse() : PostCustomerResponse(HttpStatusCode.Conflict, "Resource already exists");