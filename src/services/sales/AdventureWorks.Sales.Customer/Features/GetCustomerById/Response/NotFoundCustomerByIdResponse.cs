namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

public class NotFoundCustomerByIdResponse() : GetCustomerByIdResponse(HttpStatusCode.NotFound, Messages.NotFoundById);