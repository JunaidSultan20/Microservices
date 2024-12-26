using AdventureWorks.Contracts.Repository;

namespace AdventureWorks.Contracts.UnitOfWork;

/// <summary>
/// Represents a unit of work that coordinates the operations of repositories and manages transactions.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the generic repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity for which to get the repository.</typeparam>
    /// <returns>An instance of the repository for the specified entity type.</returns>
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    /// <summary>
    /// Commits all changes made in the current transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing the number of state entries written to the database.</returns>
    Task<int> CommitAsync();
}