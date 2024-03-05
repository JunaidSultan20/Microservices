namespace AdventureWorks.Sales.Customers.DomainEvents;

public class CustomerTerritoryIdChanged
{
    public int? OldTerritoryId { get; set; }

    public int? NewTerritoryId { get; set; }
}