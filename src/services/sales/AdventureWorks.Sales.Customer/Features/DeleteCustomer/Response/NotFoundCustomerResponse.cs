namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

public class NotFoundCustomerResponse() : DeleteCustomerResponse(HttpStatusCode.NotFound, Messages.NotFoundById);