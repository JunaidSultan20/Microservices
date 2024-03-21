namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

public class GetCustomerByIdShapedResponse(string? message, 
                                           ExpandoObject result) : ApiResponse<ExpandoObject>(HttpStatusCode.OK, message, result);