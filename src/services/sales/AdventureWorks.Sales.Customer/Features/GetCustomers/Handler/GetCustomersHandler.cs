using AdventureWorks.Sales.Customers.Features.GetCustomers.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Handler;

/// <summary>
/// Handles the request to retrieve customers with pagination, caching, and messaging functionality.
/// </summary>
/// <param name="unitOfWork">Unit of work for managing repository access.</param>
/// <param name="cache">Distributed cache for caching customer data.</param>
/// <param name="logger">Logger for logging actions and events.</param>
/// <param name="urlService">Service to handle URL-related operations.</param>
/// <param name="messageProducer">Service to send messages to a message broker.</param>
/// <remarks>
/// This handler is responsible for fetching customer data from the database, applying pagination, 
/// caching the results, and publishing a message after data retrieval. If the data is available in the cache, 
/// it returns the cached data; otherwise, it retrieves the data from the repository.
/// </remarks>
public class GetCustomersHandler(IUnitOfWork unitOfWork, 
                                 IDistributedCache cache, 
                                 ILogger<GetCustomersHandler> logger,
                                 IUrlService urlService,
                                 IMessageProducer messageProducer) : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    /// <summary>
    /// Handles the customer retrieval process, including pagination, caching, and message production.
    /// </summary>
    /// <param name="request">The request containing pagination details.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    /// A <see cref="GetCustomersResponse"/> object containing a paginated list of customers and pagination details.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the request or pagination parameters are null.</exception>
    /// <remarks>
    /// If data is cached, it returns the cached data. Otherwise, it retrieves data from the repository, caches the result, 
    /// logs the action, and sends a message using the message producer. 
    /// </remarks>
    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));
        ArgumentNullException.ThrowIfNull(nameof(request.Pagination.PageNumber));
        ArgumentNullException.ThrowIfNull(nameof(request.Pagination.PageSize));

        GetCustomersResponse? response;

        string? cacheValue = await cache.GetStringAsync(key: $"customersListP{ request.Pagination.PageNumber }S{ request.Pagination.PageSize }", 
                                                        token: cancellationToken);

        if (!string.IsNullOrEmpty(cacheValue))
        {
            response = JsonConvert.DeserializeObject<GetCustomersResponse>(cacheValue)!;
            logger.LogInformation("Returned data from the cache");
            return response;
        }

        List<Customer>? customerList = await unitOfWork.Repository<Customer>()
                                                       .GetAsync(request.Pagination.PageNumber, 
                                                                 request.Pagination.PageSize);
        int totalRecords = await unitOfWork.Repository<Customer>().GetCountAsync(customer => customer.CustomerId > 0);

        if (customerList.Count.Equals(0))
            return new NotFoundGetCustomersResponse();

        PaginationData paginationData = new PaginationData(totalRecords,
                                                           request.Pagination.PageNumber,
                                                           request.Pagination.PageSize,
                                                           urlService.GetCurrentRequestUrl());
        response = new GetCustomersResponse(customerList.Adapt<List<CustomerDto>>(), paginationData);
        DistributedCacheEntryOptions? cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(3))
                                                                                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        await cache.SetAsync($"customersListP{request.Pagination.PageNumber}S{request.Pagination.PageSize}", response, cacheEntryOptions);
        logger.LogInformation("Data saved in cache");
        await messageProducer.SendMessageAsync(Constants.SalesQueue, Constants.SalesExchange, "direct", Constants.SalesRoute, response);

        return response;
    }
}