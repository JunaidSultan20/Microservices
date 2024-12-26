namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Request;

/// <summary>
/// Provides example data for the <see cref="GetCustomersRequest"/>.
/// Implements <see cref="IExamplesProvider{T}"/> interface.
/// Returns an example of a <see cref="GetCustomersRequest"/> instance.
/// </summary>
/// <returns>A <see cref="GetCustomersRequest"/> object with example pagination parameters.</returns>
public class GetCustomersRequestExample : IExamplesProvider<GetCustomersRequest>
{
    public GetCustomersRequest GetExamples()
    {
        PaginationParameters parameters = new PaginationParameters(pageNumber: 1, pageSize: 5, fields: null);

        return new GetCustomersRequest(parameters);
    }
}