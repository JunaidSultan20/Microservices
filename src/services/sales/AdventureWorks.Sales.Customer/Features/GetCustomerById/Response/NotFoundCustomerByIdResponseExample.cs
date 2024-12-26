namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

/// <summary>
/// Provides an example of a <see cref="NotFoundCustomerByIdResponse"/>.
/// Implements <see cref="IExamplesProvider{T}"/> interface.
/// </summary>
public class NotFoundCustomerByIdResponseExample : IExamplesProvider<NotFoundCustomerByIdResponse>
{
    /// <summary>
    /// Gets an example of the <see cref="NotFoundCustomerByIdResponse"/>.
    /// </summary>
    /// <returns>An instance of <see cref="NotFoundCustomerByIdResponse"/> representing a not found response.</returns>
    public NotFoundCustomerByIdResponse GetExamples()
    {
        return new NotFoundCustomerByIdResponse();
    }
}