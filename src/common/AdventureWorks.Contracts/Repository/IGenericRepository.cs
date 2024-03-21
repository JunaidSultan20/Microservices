namespace AdventureWorks.Contracts.Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Returns records count based on predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Returns all records
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Returns list of records based on predicate provided
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Returns list of records based on predicate, page number and page size provided
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? predicate = null);

    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                 bool disableTracking = true,
                                 params Expression<Func<TEntity, object>>[]? includes);

    /// <summary>
    /// Returns record based on id provided
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync<TKey>(TKey id);

    /// <summary>
    /// Adds a new record asynchronously
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update's record
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes record
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);
}