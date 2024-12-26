namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

/// <summary>
/// Represents a shaped response for retrieving a customer by ID.
/// Inherits from <see cref="ApiResponse{T}"/> with <see cref="ExpandoObject"/> as the result type.
/// </summary>
/// <param name="message">The message associated with the response.</param>
/// <param name="result">The shaped result containing customer data.</param>
public class GetCustomerByIdShapedResponse(string? message, ExpandoObject result) : ApiResponse<ExpandoObject>(HttpStatusCode.OK, message, result);