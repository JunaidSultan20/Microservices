using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Request;

/// <summary>
/// Represents a request to retrieve a list of customers with pagination.
/// Implements <see cref="IRequest{TResponse}"/> interface.
/// Initializes a new instance of the <see cref="GetCustomersRequest"/> class.
/// </summary>
/// <param name="pagination">The pagination parameters for the request.</param>
public class GetCustomersRequest(PaginationParameters pagination) : IRequest<GetCustomersResponse>
{
    /// <summary>
    /// Gets or sets the pagination parameters for retrieving customers.
    /// </summary>
    internal PaginationParameters Pagination { get; set; } = pagination;
}