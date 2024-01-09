using AdventureWorks.Contracts.Repository;

namespace AdventureWorks.Contracts.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> CommitAsync();
}