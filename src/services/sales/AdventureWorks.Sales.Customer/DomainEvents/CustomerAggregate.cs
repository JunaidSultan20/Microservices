using AdventureWorks.Common.Events;

namespace AdventureWorks.Sales.Customers.DomainEvents;

public class CustomerAggregate : Aggregate
{
    public int CustomerId { get; set; }
    
    public int? PersonId { get; set; }
    
    public int? StoreId { get; set; }
    
    public int? TerritoryId { get; set; }
    
    public string AccountNumber { get; set; }
    
    public Guid Rowguid { get; set; }
    
    public DateTime ModifiedDate { get; set; }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case CustomerCreated customer: OnCreated(customer);
                break;
            case CustomerStoreIdChanged customer: OnStoreIdChanged(customer);
                break;
            case CustomerTerritoryIdChanged customer: OnTerritoryIdChanged(customer);
                break;
            case CustomerAccountNumberChanged customer: OnAccountNumberChanged(customer);
                break;
            default:
                throw new ArgumentException($"Unsupported event type: {@event.GetType().Name}", nameof(@event));
        }
    }

    public void CustomerCreatedEvent(int customerId, int? personId, string accountNumber, Guid rowguid, DateTime modifiedDate)
    {
        Apply(new CustomerCreated
        {
            CustomerId = customerId,
            PersonId = personId,
            AccountNumber = accountNumber,
            Rowguid = rowguid,
            ModifiedDate = modifiedDate
        });
    }

    public void CustomerStoreIdChangedEvent(int? oldStoreId, int? newStoreId)
    {
        Apply(new CustomerStoreIdChanged
        {
            OldStoreId = oldStoreId,
            NewStoreId = newStoreId
        });
    }

    public void CustomerTerritoryIdChangedEvent(int? oldTerritoryId, int? newTerritoryId)
    {
        Apply(new CustomerTerritoryIdChanged
        {
            OldTerritoryId = oldTerritoryId,
            NewTerritoryId = newTerritoryId
        });
    }

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
        Rowguid = @event.Rowguid;
        ModifiedDate = @event.ModifiedDate;
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