namespace AdventureWorks.Contracts.Repository;

/// <summary>
/// Represents a generic repository for handling basic CRUD operations and queries 
/// for entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Returns the count of records that match the given predicate.
    /// </summary>
    /// <param name="predicate">The filter expression to apply to the records.</param>
    /// <returns>A task representing the asynchronous operation, containing the count of matching records.</returns>
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Returns all records of the entity type.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing the list of all records.</returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Returns a list of records that match the given predicate.
    /// </summary>
    /// <param name="predicate">The filter expression to apply to the records.</param>
    /// <returns>A task representing the asynchronous operation, containing the list of matching records.</returns>
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Returns a paginated list of records that match the given predicate, or all records if the predicate is null.
    /// </summary>
    /// <param name="pageNumber">The number of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="predicate">The filter expression to apply to the records (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the paginated list of matching records.</returns>
    Task<List<TEntity>> GetAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Returns a list of records based on the given filter, order, and other query options.
    /// </summary>
    /// <param name="predicate">The filter expression to apply to the records (optional).</param>
    /// <param name="orderBy">The ordering function to apply to the records (optional).</param>
    /// <param name="disableTracking">Specifies whether to disable entity tracking (default is true).</param>
    /// <param name="includes">The related entities to include in the query (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the list of matching records.</returns>
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                 bool disableTracking = true,
                                 params Expression<Func<TEntity, object>>[]? includes);

    /// <summary>
    /// Returns a record that matches the provided id.
    /// </summary>
    /// <typeparam name="TKey">The type of the id key.</typeparam>
    /// <param name="id">The id of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, containing the entity if found, or null otherwise.</returns>
    Task<TEntity?> GetByIdAsync<TKey>(TKey id);

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the added entity.</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(TEntity entity);
}