namespace AdventureWorks.Sales.Customers.DomainEvents;

public class CustomerAccountNumberChanged
{
    public string OldAccountNumber { get; set; } = string.Empty;

    public string NewAccountNumber { get; set; } = string.Empty; 
}