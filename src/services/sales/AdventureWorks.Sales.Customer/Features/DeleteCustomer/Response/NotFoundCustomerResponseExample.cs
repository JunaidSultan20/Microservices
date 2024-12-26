namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;

/// <summary>
/// Provides example data for a <see cref="NotFoundCustomerResponse"/>.
/// </summary>
public class NotFoundCustomerResponseExample : IExamplesProvider<NotFoundCustomerResponse>
{
    /// <summary>
    /// Returns an example of a <see cref="NotFoundCustomerResponse"/>.
    /// </summary>
    /// <returns>An instance of <see cref="NotFoundCustomerResponse"/>.</returns>
    public NotFoundCustomerResponse GetExamples()
    {
        return new NotFoundCustomerResponse();
    }
}