using AdventureWorks.Common.Constants;
using Microsoft.Extensions.Logging;

namespace Sales.Application.Features.Customers.Handlers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginationResponse<IEnumerable<CustomerWithLinksDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IUrlService _urlService;
    private readonly ILogger<GetAllCustomersQueryHandler> _logger;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, IUrlService urlService, ILogger<GetAllCustomersQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
        _urlService = urlService;
        _logger = logger;
    }

    public async Task<PaginationResponse<IEnumerable<CustomerWithLinksDto>>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken = default)
    {
        PaginationResponse<IEnumerable<CustomerWithLinksDto>> customersPagination =
            await _cacheService.GetAsync<PaginationResponse<IEnumerable<CustomerWithLinksDto>>>(
                $"customersListP{request.PageNumber}S{request.PageSize}");

        if (customersPagination != null)
        {
            _logger.LogInformation("Result returned from Redis cache");
            return customersPagination;
        }

        IEnumerable<Customer>? result =
            await _unitOfWork.ICustomerRepository.GetAsync(customer => customer.CustomerId > 0,
                request.PageNumber ?? Constants.DefaultPageNumber,
                request.PageSize ?? Constants.DefaultPageSize);

        if (!result.Any())
        {
            return new PaginationResponse<IEnumerable<CustomerWithLinksDto>>(HttpStatusCode.NotFound,
                "No Customer Record Exists In Database.", null, null);
        }

        PaginationData paginationData =
            new PaginationData(_unitOfWork.ICustomerRepository.GetCount(customer => customer.CustomerId > 0).Result,
                request.PageNumber, request.PageSize, _urlService.GetCurrentRequestUrl());

        BaseResponse<IEnumerable<CustomerDto>> response =
            new BaseResponse<IEnumerable<CustomerDto>>(HttpStatusCode.OK, null,
                _mapper.Map<IEnumerable<CustomerDto>>(result));

        customersPagination = new PaginationResponse<IEnumerable<CustomerWithLinksDto>>(HttpStatusCode.OK, null,
            _mapper.Map<IEnumerable<CustomerWithLinksDto>>(result), paginationData);

        await _cacheService.SetAsync($"customersListP{request.PageNumber}S{request.PageSize}", customersPagination);
        return customersPagination;
    }
}