namespace AdventureWorks.Sales.Customers.Dto;

public class UpdateCustomerDto
{
    /// <summary>
    /// Person id field.
    /// </summary>
    [JsonProperty(PropertyName = "personId", Order = 1)]
    public int? PersonId { get; set; }

    /// <summary>
    /// Store id field.
    /// </summary>
    [JsonProperty(PropertyName = "storeId", Order = 2)]
    public int? StoreId { get; set; }

    /// <summary>
    /// Territory id field.
    /// </summary>
    [JsonProperty(PropertyName = "territoryId", Order = 3)]
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Account number field.
    /// </summary>
    [JsonProperty(PropertyName = "accountNumber", Order = 4)]
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Modified date field.
    /// </summary>
    [JsonProperty(PropertyName = "modifiedDate", Order = 5)]
    public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

    public UpdateCustomerDto(int? personId,
                             int? storeId,
                             int? territoryId,
                             string? accountNumber)
        => (PersonId, StoreId, TerritoryId, AccountNumber)
            = (personId, storeId, territoryId, accountNumber);
}