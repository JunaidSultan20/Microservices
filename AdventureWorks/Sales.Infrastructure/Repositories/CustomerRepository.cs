namespace Sales.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AdventureWorksSalesContext context) : base(context)
    {
    }
}