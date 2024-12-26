namespace AdventureWorks.Sales.Customers.DomainEvents;

/// <summary>
/// Represents a domain event that changes the customer's territory ID.
/// </summary>
public class CustomerTerritoryIdChanged
{
    /// <summary>
    /// Gets or sets the old territory ID.
    /// </summary>
    public int? OldTerritoryId { get; set; }

    /// <summary>
    /// Gets or sets the new territory ID.
    /// </summary>
    public int? NewTerritoryId { get; set; }
}