namespace AdventureWorks.Sales.Customers.Dto;

/// <summary>
/// Data Transfer Object representing customer details.
/// </summary>
/// <param name="customerId">The unique identifier for the customer.</param>
/// <param name="personId">The identifier for the associated person, if applicable.</param>
/// <param name="storeId">The identifier for the store associated with the customer, if applicable.</param>
/// <param name="territoryId">The identifier for the territory associated with the customer, if applicable.</param>
/// <param name="accountNumber">The account number for the customer.</param>
/// <param name="modifiedDate">The date and time when the customer record was last modified.</param>
/// <param name="links">A list of HATEOAS links associated with the customer.</param>
public class CustomerDto(int customerId,
                         int? personId,
                         int? storeId,
                         int? territoryId,
                         string? accountNumber,
                         DateTime? modifiedDate,
                         IReadOnlyList<Links>? links = null)
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    [JsonProperty(PropertyName = "customerId", Order = 1)]
    public int CustomerId { get; set; } = customerId;

    /// <summary>
    /// Gets or sets the identifier for the associated person, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "personId", Order = 2)]
    public int? PersonId { get; set; } = personId;

    /// <summary>
    /// Gets or sets the identifier for the store associated with the customer, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "storeId", Order = 3)]
    public int? StoreId { get; set; } = storeId;

    /// <summary>
    /// Gets or sets the identifier for the territory associated with the customer, if applicable.
    /// </summary>
    [JsonProperty(PropertyName = "territoryId", Order = 4)]
    public int? TerritoryId { get; set; } = territoryId;

    /// <summary>
    /// Gets or sets the account number for the customer.
    /// </summary>
    [JsonProperty(PropertyName = "accountNumber", Order = 5)]
    public string? AccountNumber { get; set; } = accountNumber;

    /// <summary>
    /// Gets or sets the date and time when the customer record was last modified.
    /// </summary>
    [JsonProperty(PropertyName = "modifiedDate", Order = 6)]
    public DateTime? ModifiedDate { get; set; } = modifiedDate;

    /// <summary>
    /// Gets or sets the HATEOAS links associated with the customer.
    /// </summary>
    [JsonProperty(PropertyName = "links", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
    public IReadOnlyList<Links>? Links { get; set; } = links;
}