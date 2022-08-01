using AdventureWorks.Common.Constants;

namespace Sales.Application.Features;

public class BaseHandler
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected BaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork ?? throw new Exception(Messages.ArgumentNullExceptionMessage,
            new ArgumentNullException(nameof(unitOfWork)));
        Mapper = mapper ?? throw new Exception(Messages.ArgumentNullExceptionMessage,
            new ArgumentNullException(nameof(mapper)));
    }
}