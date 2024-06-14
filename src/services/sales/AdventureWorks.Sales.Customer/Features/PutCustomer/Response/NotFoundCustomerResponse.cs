namespace AdventureWorks.Sales.Customers.Features.PutCustomer.Response;

public class NotFoundCustomerResponse() : PutCustomerResponse(HttpStatusCode.NotFound, Messages.NotFoundById);