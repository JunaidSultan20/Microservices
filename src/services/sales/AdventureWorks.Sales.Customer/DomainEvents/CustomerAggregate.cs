using AdventureWorks.Common.Events;

namespace AdventureWorks.Sales.Customers.DomainEvents;

/// <summary>
/// Represents an aggregate root for customer-related events in the domain model.
/// </summary>
public class CustomerAggregate : Aggregate
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the associated person, if applicable.
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the store associated with the customer, if applicable.
    /// </summary>
    public int? StoreId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the territory associated with the customer, if applicable.
    /// </summary>
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Gets or sets the account number assigned to the customer.
    /// </summary>
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the globally unique identifier for the customer record.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time the customer record was last modified.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Handles events applied to the aggregate and updates its state.
    /// </summary>
    /// <param name="@event">The event to handle.</param>
    protected override void When(object @event)
    {
        switch (@event)
        {
            case CustomerCreated customer:
                OnCreated(customer);
                break;
            case CustomerStoreIdChanged customer:
                OnStoreIdChanged(customer);
                break;
            case CustomerTerritoryIdChanged customer:
                OnTerritoryIdChanged(customer);
                break;
            case CustomerAccountNumberChanged customer:
                OnAccountNumberChanged(customer);
                break;
            default:
                throw new ArgumentException($"Unsupported event type: {@event.GetType().Name}", nameof(@event));
        }
    }

    /// <summary>
    /// Applies the <see cref="CustomerCreated"/> event to the aggregate.
    /// </summary>
    /// <param name="customerId">The unique identifier for the customer.</param>
    /// <param name="personId">The identifier for the associated person.</param>
    /// <param name="accountNumber">The account number assigned to the customer.</param>
    public void CustomerCreatedEvent(int customerId, int? personId, string accountNumber)
    {
        Apply(new CustomerCreated
        {
            CustomerId = customerId,
            PersonId = personId,
            AccountNumber = accountNumber
        });
    }

    /// <summary>
    /// Applies the <see cref="CustomerStoreIdChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="oldStoreId">The old store identifier.</param>
    /// <param name="newStoreId">The new store identifier.</param>
    public void CustomerStoreIdChangedEvent(int? oldStoreId, int? newStoreId)
    {
        Apply(new CustomerStoreIdChanged
        {
            OldStoreId = oldStoreId,
            NewStoreId = newStoreId
        });
    }

    /// <summary>
    /// Applies the <see cref="CustomerTerritoryIdChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="oldTerritoryId">The old territory identifier.</param>
    /// <param name="newTerritoryId">The new territory identifier.</param>
    public void CustomerTerritoryIdChangedEvent(int? oldTerritoryId, int? newTerritoryId)
    {
        Apply(new CustomerTerritoryIdChanged
        {
            OldTerritoryId = oldTerritoryId,
            NewTerritoryId = newTerritoryId
        });
    }

    /// <summary>
    /// Applies the <see cref="CustomerAccountNumberChanged"/> event to the aggregate.
    /// </summary>
    /// <param name="oldAccountNumber">The old account number.</param>
    /// <param name="newAccountNumber">The new account number.</param>
    public void CustomerAccountNumberChangedEvent(string oldAccountNumber, string newAccountNumber)
    {
        Apply(new CustomerAccountNumberChanged
        {
            OldAccountNumber = oldAccountNumber,
            NewAccountNumber = newAccountNumber
        });
    }

    #region Event Handlers

    private void OnCreated(CustomerCreated @event)
    {
        CustomerId = @event.CustomerId;
        PersonId = @event.PersonId;
        AccountNumber = @event.AccountNumber;
    }

    private void OnStoreIdChanged(CustomerStoreIdChanged @event)
    {
        StoreId = @event.NewStoreId;
    }

    private void OnTerritoryIdChanged(CustomerTerritoryIdChanged @event)
    {
        TerritoryId = @event.NewTerritoryId;
    }

    private void OnAccountNumberChanged(CustomerAccountNumberChanged @event)
    {
        AccountNumber = @event.NewAccountNumber;
    }

    #endregion
}