using AdventureWorks.Sales.Customers.Features.GetCustomers.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Handler;

public class GetCustomersHandler(IUnitOfWork unitOfWork, 
                                 IDistributedCache cache, 
                                 ILogger<GetCustomersHandler> logger,
                                 IUrlService urlService,
                                 IMessageProducer messageProducer) : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        ArgumentNullException.ThrowIfNull(nameof(request.Pagination.PageNumber));

        ArgumentNullException.ThrowIfNull(nameof(request.Pagination.PageSize));

        GetCustomersResponse? response;

        string? cacheValue = await cache.GetStringAsync(key: $"customersListP{request.Pagination.PageNumber}S{request.Pagination.PageSize}",
                                                         token: cancellationToken);

        if (!string.IsNullOrEmpty(cacheValue))
        {
            response = JsonConvert.DeserializeObject<GetCustomersResponse>(cacheValue)!;
            logger.LogInformation("Returned data from the cache");
            return response;
        }

        var customerList = await unitOfWork.Repository<Customer>()
                                           .GetAsync(request.Pagination.PageNumber,
                                                     request.Pagination.PageSize);

        var count = await unitOfWork.Repository<Customer>().GetCountAsync(customer => customer.CustomerId > 0);

        if (customerList.Count.Equals(0))
            return new NotFoundGetCustomersResponse();

        PaginationData paginationData = new PaginationData(count,
                                                           request.Pagination.PageNumber,
                                                           request.Pagination.PageSize,
                                                           urlService.GetCurrentRequestUrl());

        response = new GetCustomersResponse(customerList.Adapt<List<CustomerDto>>(), paginationData);

        var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(3))
                                                                  .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        await cache.SetAsync($"customersListP{request.Pagination.PageNumber}S{request.Pagination.PageSize}", response, cacheEntryOptions);

        logger.LogInformation("Data saved in cache");

        messageProducer.SendMessageAsync(Constants.SalesQueue,
                                    "SalesExchange",
                                    "direct",
                                    "sales_route",
                                    response);

        return response;
    }
}