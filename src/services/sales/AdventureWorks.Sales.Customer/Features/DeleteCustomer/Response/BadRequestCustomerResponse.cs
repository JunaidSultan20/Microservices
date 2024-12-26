namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

/// <summary>
/// Represents a response indicating that a bad request occurred when attempting to delete a customer.
/// Inherits from <see cref="DeleteCustomerResponse"/>.
/// Initializes a new instance of the <see cref="BadRequestCustomerResponse"/> class.
/// </summary>
public class BadRequestCustomerResponse() : DeleteCustomerResponse(HttpStatusCode.BadRequest, Messages.UnableToDeleteRecord);