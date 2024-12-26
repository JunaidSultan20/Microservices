namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

/// <summary>
/// Represents the response returned after attempting to delete a customer.
/// </summary>
public class DeleteCustomerResponse : ApiResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCustomerResponse"/> class with a default 
    /// "No Content" status code and a "Record Deleted" message.
    /// </summary>
    public DeleteCustomerResponse() : base(HttpStatusCode.NoContent, Messages.RecordDeleted)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCustomerResponse"/> class with the specified
    /// status code and message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code to return.</param>
    /// <param name="message">The message to return.</param>
    protected DeleteCustomerResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
}