namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

public class BadRequestCustomerResponse(string message) : PutCustomerResponse(HttpStatusCode.BadRequest, message);