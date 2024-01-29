using AdventureWorks.Sales.Customers.Features.GetCustomers.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Customers.Features.GetCustomers.Handler;

public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedCache _cache;
    private readonly ILogger<GetCustomersHandler> _logger;
    private readonly IUrlService _urlService;
    private readonly IMessageProducer _messageProducer;
    private readonly RabbitMqOptions _rabbitMqConfig;

    public GetCustomersHandler(IUnitOfWork unitOfWork,
                               IDistributedCache cache,
                               ILogger<GetCustomersHandler> logger,
                               IUrlService urlService,
                               IMessageProducer messageProducer,
                               IOptions<RabbitMqOptions> options)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _logger = logger;
        _urlService = urlService;
        _messageProducer = messageProducer;
        _rabbitMqConfig = options.Value;
    }

    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        ArgumentNullException.ThrowIfNull(nameof(request.Pagination.PageNumber));

        ArgumentNullException.ThrowIfNull(nameof(request.Pagination.PageSize));

        GetCustomersResponse? response;

        string? cacheValue = await _cache.GetStringAsync(key: $"customersListP{request.Pagination.PageNumber}S{request.Pagination.PageSize}",
                                                         token: cancellationToken);

        if (!string.IsNullOrEmpty(cacheValue))
        {
            response = JsonConvert.DeserializeObject<GetCustomersResponse>(cacheValue)!;
            _logger.LogInformation("Returned data from the cache");
            return response;
        }

        var customerList = await _unitOfWork.Repository<Domain.Entities.Customer>()
                                            .GetAsync(request.Pagination.PageNumber,
                                                      request.Pagination.PageSize);

        var count = await _unitOfWork.Repository<Domain.Entities.Customer>().GetCountAsync(customer => customer.CustomerId > 0);

        if (customerList.Count.Equals(0))
            return new NotFoundGetCustomersResponse();

        PaginationData paginationData = new PaginationData(count,
                                                           request.Pagination.PageNumber,
                                                           request.Pagination.PageSize,
                                                           _urlService.GetCurrentRequestUrl());

        response = new GetCustomersResponse(customerList.Adapt<List<CustomerDto>>(), paginationData);

        var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(3))
                                                                  .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        await _cache.SetAsync($"customersListP{request.Pagination.PageNumber}S{request.Pagination.PageSize}", response, cacheEntryOptions);

        _logger.LogInformation("Data saved in cache");

        _messageProducer.SendMessage(_rabbitMqConfig,
                                     Constants.SalesQueue,
                                     "SalesExchange",
                                     "direct",
                                     "sales_route",
                                     response);

        return response;
    }
}