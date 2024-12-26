namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

/// <summary>
/// Represents a response indicating that a customer was not found by ID.
/// Inherits from <see cref="GetCustomerByIdResponse"/>.
/// Initializes a new instance of the <see cref="NotFoundCustomerByIdResponse"/> class.
/// </summary>
public class NotFoundCustomerByIdResponse() : GetCustomerByIdResponse(HttpStatusCode.NotFound, Messages.NotFoundById);