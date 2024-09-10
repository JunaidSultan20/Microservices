namespace AdventureWorks.Sales.Customers.DomainEvents;

public class CustomerCreated
{
    public int CustomerId { get; set; }

    public int? PersonId { get; set; }

    public string AccountNumber { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }
}