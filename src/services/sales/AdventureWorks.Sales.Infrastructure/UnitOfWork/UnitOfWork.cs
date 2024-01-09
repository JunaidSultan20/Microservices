namespace AdventureWorks.Sales.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AdventureWorksSalesContext _context;
    private readonly Dictionary<Type, object?> _repositories;
    private readonly IDbContextTransaction _transactionScope;

    public UnitOfWork(AdventureWorksSalesContext context)
    {
        _context = context;
        _transactionScope = _context.Database.BeginTransaction();
        _repositories = new Dictionary<Type, object?>();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories.Keys.Contains(typeof(TEntity)))
        {
            return _repositories[typeof(TEntity)] as IGenericRepository<TEntity> ?? throw new InvalidOperationException();
        }
        IGenericRepository<TEntity> repository = new GenericRepository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public async Task<int> CommitAsync()
    {
        List<Task> tasks = new List<Task>
        {
            _context.SaveChangesAsync(),
            _transactionScope.CommitAsync()
        };

        try
        {
            await Task.WhenAll(tasks);
            return 1;
        }
        catch (Exception)
        {
            await _transactionScope.RollbackAsync();
            throw;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}