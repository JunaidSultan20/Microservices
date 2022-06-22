namespace Sales.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AdventureWorksSalesContext _context;

    public Repository(AdventureWorksSalesContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
    {
        return await Task.Run(() => _context.Set<TEntity>().Where(predicate).Count());
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
    {
        return await _context.Set<TEntity>().Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
        bool disableTracking = true, params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes is not null)
            foreach (Expression<Func<TEntity, object>>? statement in includes)
            {
                query = _context.Set<TEntity>().Include(statement);
            }

        if (predicate is not null) query = query.Where(predicate);

        if (orderBy is not null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync<TKey>(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}