namespace AdventureWorks.Sales.Customers;

public abstract class BaseHandler<T>(IUnitOfWork unitOfWork, IDistributedCache cache, ILogger<T> logger) where T : class
{
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IDistributedCache Cache = cache;
    protected readonly ILogger<T> Logger = logger;
}