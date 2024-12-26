namespace AdventureWorks.Sales.Customers.DomainEvents;

/// <summary>
/// Represents a domain event that changes the customer's store ID.
/// </summary>
public class CustomerStoreIdChanged
{
    /// <summary>
    /// Gets or sets the new store ID.
    /// </summary>
    public int? NewStoreId { get; set; }

    /// <summary>
    /// Gets or sets the old store ID.
    /// </summary>
    public int? OldStoreId { get; set; }
}