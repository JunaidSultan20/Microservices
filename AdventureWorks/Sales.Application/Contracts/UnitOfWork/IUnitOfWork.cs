namespace Sales.Application.Contracts.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository ICustomerRepository { get; }

    Task<int> Commit();
}