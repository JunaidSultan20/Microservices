namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;

/// <summary>
/// Provides example data for the <see cref="GetCustomerByIdRequest"/> class.
/// </summary>
public class GetCustomerByIdRequestExample : IExamplesProvider<GetCustomerByIdRequest>
{
    /// <summary>
    /// Gets an example of a <see cref="GetCustomerByIdRequest"/>.
    /// </summary>
    /// <returns>An example of a <see cref="GetCustomerByIdRequest"/> with a sample customer ID.</returns>
    public GetCustomerByIdRequest GetExamples()
    {
        return new GetCustomerByIdRequest(id: 1);
    }
}
