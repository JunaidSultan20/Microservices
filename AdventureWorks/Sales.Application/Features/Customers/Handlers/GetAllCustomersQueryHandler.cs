namespace Sales.Application.Features.Customers.Handlers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginationResponse<IEnumerable<CustomerDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IUrlService _urlService;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, IUrlService urlService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
        _urlService = urlService;
    }

    public async Task<PaginationResponse<IEnumerable<CustomerDto>>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken = default)
    {
        PaginationResponse<IEnumerable<CustomerDto>> customersPagination =
            await _cacheService.GetAsync<PaginationResponse<IEnumerable<CustomerDto>>>(
                $"customersListP{request.PageNumber}S{request.PageSize}");

        if (customersPagination != null)
        {
            return customersPagination;
        }

        IEnumerable<Customer>? result =
            await _unitOfWork.ICustomerRepository.GetAsync(customer => customer.CustomerId > 0, request.PageNumber.Value,
                request.PageSize.Value);

        if (!result.Any())
        {
            return customersPagination = new PaginationResponse<IEnumerable<CustomerDto>>(HttpStatusCode.NotFound,
                "No Customer Record Exists In Database.", null, null);
        }

        PaginationData paginationData =
            new PaginationData(_unitOfWork.ICustomerRepository.GetCount(customer => customer.CustomerId > 0).Result,
                request.PageNumber, request.PageSize, _urlService.GetCurrentRequestUrl());

        //result = result?.Skip((request.PageNumber.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value);

        BaseResponse<IEnumerable<CustomerDto>> response =
            new BaseResponse<IEnumerable<CustomerDto>>(HttpStatusCode.OK, null,
                _mapper.Map<IEnumerable<CustomerDto>>(result));

        customersPagination = new PaginationResponse<IEnumerable<CustomerDto>>(HttpStatusCode.OK, null,
            _mapper.Map<IEnumerable<CustomerDto>>(result), paginationData);

        await _cacheService.SetAsync($"customersListP{request.PageNumber}S{request.PageSize}", customersPagination);
        return customersPagination;
    }
}