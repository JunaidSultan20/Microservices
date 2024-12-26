namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

/// <summary>
/// Represents a response indicating that a customer was not found. Initializes a new instance of the <see cref="NotFoundCustomerResponse"/> class with a "Not Found" status.
/// </summary>
public class NotFoundCustomerResponse() : DeleteCustomerResponse(HttpStatusCode.NotFound, Messages.NotFoundById);