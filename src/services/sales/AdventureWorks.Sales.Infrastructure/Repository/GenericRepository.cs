namespace AdventureWorks.Sales.Infrastructure.Repository;

/// <summary>
/// Represents a generic repository implementation for performing CRUD operations on a specified entity type.
/// </summary>
/// <typeparam name="TEntity">The type of the entity for the repository.</typeparam>
public class GenericRepository<TEntity>(AdventureWorksSalesContext context) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AdventureWorksSalesContext _context = context ?? throw new ArgumentNullException(paramName: nameof(context));

    /// <summary>
    /// Gets the count of entities matching the given predicate.
    /// </summary>
    /// <param name="predicate">A function to filter the entities.</param>
    /// <returns>The count of entities that match the predicate.</returns>
    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>()
                         .Where(predicate)
                         .CountAsync();

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> from the database.
    /// </summary>
    /// <returns>A list of all entities of type <typeparamref name="TEntity"/>.</returns>
    public async Task<List<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>()
                         .ToListAsync();

    /// <summary>
    /// Retrieves a list of entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">A function to filter the entities.</param>
    /// <returns>A list of entities that match the predicate.</returns>
    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>()
                         .Where(predicate)
                         .ToListAsync();

    /// <summary>
    /// Retrieves a paginated list of entities that match the given predicate.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="predicate">Optional: A function to filter the entities.</param>
    /// <returns>A paginated list of entities that match the predicate.</returns>
    public async Task<List<TEntity>> GetAsync(int pageNumber, int pageSize,
                                              Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query.Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize)
                          .AsNoTracking()
                          .ToListAsync();
    }

    /// <summary>
    /// Retrieves a list of entities that match the given conditions, including pagination, ordering, and optional tracking.
    /// </summary>
    /// <param name="predicate">Optional: A function to filter the entities.</param>
    /// <param name="orderBy">Optional: A function to order the entities.</param>
    /// <param name="disableTracking">Indicates whether tracking should be disabled. Default is true.</param>
    /// <param name="includes">Optional: Related entities to include in the query.</param>
    /// <returns>A list of entities that match the conditions.</returns>
    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              bool disableTracking = true,
                                              params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (includes is not null)
        {
            foreach (Expression<Func<TEntity, object>>? statement in includes)
            {
                query = _context.Set<TEntity>().Include(statement);
            }
        }

        if (predicate is not null)
            query = query.Where(predicate);

        return orderBy is not null
            ? await orderBy(query).ToListAsync()
            : await query.ToListAsync();
    }

    /// <summary>
    /// Retrieves an entity by its primary key.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    /// <param name="id">The primary key of the entity.</param>
    /// <returns>The entity with the given primary key, or null if not found.</returns>
    public async Task<TEntity?> GetByIdAsync<TKey>(TKey id)
        => await _context.Set<TEntity>()
                         .FindAsync(id);

    /// <summary>
    /// Adds a new entity to the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The entity that was added.</returns>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
}