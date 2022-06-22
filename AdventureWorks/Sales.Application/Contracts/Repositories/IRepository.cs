namespace Sales.Application.Contracts.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);

    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool disableTracking = true,
        params Expression<Func<TEntity, object>>[]? includes);

    Task<TEntity?> GetByIdAsync<TKey>(TKey id);

    Task<TEntity> AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}