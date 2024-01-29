namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

public class DeleteCustomerResponse : ApiResult
{
    public DeleteCustomerResponse() : base(HttpStatusCode.NoContent, Messages.RecordDeleted)
    {
    }

    protected DeleteCustomerResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
}