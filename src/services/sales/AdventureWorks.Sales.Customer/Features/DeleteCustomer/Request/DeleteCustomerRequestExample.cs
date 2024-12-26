namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;

/// <summary>
/// Provides example data for the <see cref="DeleteCustomerRequest"/> class.
/// </summary>
public class DeleteCustomerRequestExample : IExamplesProvider<DeleteCustomerRequest>
{
    /// <summary>
    /// Gets an example of a <see cref="DeleteCustomerRequest"/>.
    /// </summary>
    /// <returns>An example of a <see cref="DeleteCustomerRequest"/> with a sample customer ID.</returns>
    public DeleteCustomerRequest GetExamples()
    {
        return new DeleteCustomerRequest(id: 1);
    }
}