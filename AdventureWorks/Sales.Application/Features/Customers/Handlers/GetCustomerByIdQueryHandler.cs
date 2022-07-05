using AdventureWorks.Common.Helpers;

namespace Sales.Application.Features.Customers.Handlers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUrlService _urlService;

    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUrlService urlService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _urlService = urlService;
    }

    public async Task<BaseResponse<CustomerDto>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Customer? result = await _unitOfWork.ICustomerRepository.GetByIdAsync(request.Id);
        if (result != null)
        {
            IReadOnlyList<Links> links = LinksHelper.CreateLinks(request.Id, null, _urlService);
            return new BaseResponse<CustomerDto>(HttpStatusCode.OK, null, _mapper.Map<CustomerDto>(result)){Links = links};
        }
        return new BaseResponse<CustomerDto>(HttpStatusCode.NotFound, null, null);
    }
}