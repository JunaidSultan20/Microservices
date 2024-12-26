namespace AdventureWorks.Sales.Infrastructure.UnitOfWork;

/// <summary>
/// Represents the unit of work implementation for managing repositories and database transactions.
/// </summary>
/// <remarks>
/// This class is responsible for coordinating repository operations and handling transactions 
/// to ensure that changes to the database are committed or rolled back as a single unit of work.
/// </remarks>
public class UnitOfWork : IUnitOfWork
{
    private readonly AdventureWorksSalesContext _context;
    private readonly Dictionary<Type, object?> _repositories;
    private readonly IDbContextTransaction _transactionScope;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The database context used to interact with the data source.</param>
    public UnitOfWork(AdventureWorksSalesContext context)
    {
        _context = context;
        _transactionScope = _context.Database.BeginTransaction();
        _repositories = new Dictionary<Type, object?>();
    }

    /// <summary>
    /// Retrieves the repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity for which the repository is retrieved.</typeparam>
    /// <returns>An instance of <see cref="IGenericRepository{TEntity}"/> for the specified entity type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the repository cannot be retrieved.
    /// </exception>
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

    /// <summary>
    /// Commits all changes made to the database in a single transaction asynchronously.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. Returns 1 if the commit is successful.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown when an error occurs during the commit, causing the transaction to be rolled back.
    /// </exception>
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

    /// <summary>
    /// Disposes the database context and suppresses finalization.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}