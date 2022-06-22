using System.Transactions;
using Sales.Application.Contracts.UnitOfWork;
using Sales.Infrastructure.Repositories;

namespace Sales.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AdventureWorksSalesContext _context;
    private bool _disposed = false;
    private CustomerRepository? _customerRepository = null;

    public UnitOfWork(AdventureWorksSalesContext context)
    {
        _context = context;
    }

    public ICustomerRepository ICustomerRepository
    {
        get
        {
            if (_customerRepository is null)
                _customerRepository = new CustomerRepository(_context);
            return _customerRepository;
        }
    }

    public async Task<int> Commit()
    {
        try
        {
            int result;
            TransactionScope transactionScope;
            using (transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _context.SaveChangesAsync();
                transactionScope.Complete();
            }
            transactionScope.Dispose();
            return result;
        }
        catch (Exception exception)
        {
            Rollback();
            throw;
        }
    }

    private void Rollback()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}