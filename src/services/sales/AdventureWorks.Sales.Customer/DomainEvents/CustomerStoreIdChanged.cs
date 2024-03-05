namespace AdventureWorks.Sales.Customers.DomainEvents;

public class CustomerStoreIdChanged
{
    public int? NewStoreId { get; set; }

    public int? OldStoreId { get; set; }
}