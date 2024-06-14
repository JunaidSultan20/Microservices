namespace AdventureWorks.Sales.Infrastructure.Repository;

public class GenericRepository<TEntity>(AdventureWorksSalesContext context) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AdventureWorksSalesContext _context = context ?? throw new ArgumentNullException(paramName: nameof(context));

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>()
                         .Where(predicate)
                         .CountAsync();

    public async Task<List<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>()
                         .ToListAsync();

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>()
                         .Where(predicate)
                         .ToListAsync();

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

    public async Task<TEntity?> GetByIdAsync<TKey>(TKey id)
        => await _context.Set<TEntity>()
                         .FindAsync(id);

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
}