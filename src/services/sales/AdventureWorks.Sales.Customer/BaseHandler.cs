namespace AdventureWorks.Sales.Customers;

public abstract class BaseHandler<T> where T : class
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IDistributedCache Cache;
    protected readonly ILogger<T> Logger;

    protected BaseHandler(IUnitOfWork unitOfWork, IDistributedCache cache, ILogger<T> logger)
    {
        UnitOfWork = unitOfWork;
        Cache = cache;
        Logger = logger;
    }
}