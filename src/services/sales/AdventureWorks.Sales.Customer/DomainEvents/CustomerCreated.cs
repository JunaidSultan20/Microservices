namespace AdventureWorks.Sales.Customers.DomainEvents;

/// <summary>
/// Represents an event that occurs when a customer is created.
/// </summary>
public class CustomerCreated
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the person associated with the customer, if applicable.
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Gets or sets the account number assigned to the customer.
    /// </summary>
    public string AccountNumber { get; set; } = string.Empty;
}