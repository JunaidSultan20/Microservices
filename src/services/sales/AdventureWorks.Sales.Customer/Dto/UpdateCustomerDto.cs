namespace AdventureWorks.Sales.Customers.Dto;

/// <summary>
/// Data Transfer Object for updating customer information.
/// </summary>
public class UpdateCustomerDto
{
    /// <summary>
    /// Gets or sets the identifier for the associated person, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "personId", Order = 1)]
    public int? PersonId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the store associated with the customer, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "storeId", Order = 2)]
    public int? StoreId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the territory associated with the customer, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "territoryId", Order = 3)]
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Gets or sets the account number for the customer.
    /// </summary>
    [JsonProperty(PropertyName = "accountNumber", Order = 4)]
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the customer record was last modified.
    /// Defaults to the current UTC date and time.
    /// </summary>
    [JsonProperty(PropertyName = "modifiedDate", Order = 5)]
    public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCustomerDto"/> class with the specified values.
    /// </summary>
    /// <param name="personId">The identifier for the associated person.</param>
    /// <param name="storeId">The identifier for the associated store.</param>
    /// <param name="territoryId">The identifier for the associated territory.</param>
    /// <param name="accountNumber">The account number of the customer.</param>
    public UpdateCustomerDto(int? personId,
                             int? storeId,
                             int? territoryId,
                             string? accountNumber)
        => (PersonId, StoreId, TerritoryId, AccountNumber)
            = (personId, storeId, territoryId, accountNumber);
}