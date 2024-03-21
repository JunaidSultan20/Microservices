namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

public class BadRequestCustomerResponse() : DeleteCustomerResponse(HttpStatusCode.BadRequest, Messages.UnableToDeleteRecord);