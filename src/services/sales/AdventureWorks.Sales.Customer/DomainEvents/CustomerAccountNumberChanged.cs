namespace AdventureWorks.Sales.Customers.DomainEvents;

/// <summary>
/// Represents a domain event that changes the customer's account number.
/// </summary>
public class CustomerAccountNumberChanged
{
    /// <summary>
    /// Gets or sets the old account number.
    /// </summary>
    public string OldAccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new account number.
    /// </summary>
    public string NewAccountNumber { get; set; } = string.Empty;
}