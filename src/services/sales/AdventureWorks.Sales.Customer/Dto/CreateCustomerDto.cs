namespace AdventureWorks.Sales.Customers.Dto;

/// <summary>
/// Data Transfer Object for creating a new customer.
/// </summary>
public class CreateCustomerDto
{
    /// <summary>
    /// Gets or sets the identifier for the associated person, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "personId", Order = 2)]
    public int? PersonId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the store associated with the customer, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "storeId", Order = 3)]
    public int? StoreId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the territory associated with the customer, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "territoryId", Order = 4)]
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Gets or sets the account number for the customer.
    /// </summary>
    [JsonProperty(PropertyName = "accountNumber", Order = 5)]
    public string? AccountNumber { get; set; }
}