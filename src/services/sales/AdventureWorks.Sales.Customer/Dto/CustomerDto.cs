namespace AdventureWorks.Sales.Customers.Dto;

/// <summary>
/// Customer Dto class that returns customer details.
/// </summary>
public class CustomerDto(int customerId,
                         int? personId,
                         int? storeId,
                         int? territoryId,
                         string? accountNumber,
                         DateTime? modifiedDate,
                         IReadOnlyList<Links>? links = null)
{
    /// <summary>
    /// Customer id field.
    /// </summary>
    [JsonProperty(PropertyName = "customerId", Order = 1)]
    public int CustomerId { get; set; } = customerId;

    /// <summary>
    /// Person id field.
    /// </summary>
    [JsonProperty(PropertyName = "personId", Order = 2)]
    public int? PersonId { get; set; } = personId;

    /// <summary>
    /// Store id field.
    /// </summary>
    [JsonProperty(PropertyName = "storeId", Order = 3)]
    public int? StoreId { get; set; } = storeId;

    /// <summary>
    /// Territory id field.
    /// </summary>
    [JsonProperty(PropertyName = "territoryId", Order = 4)]
    public int? TerritoryId { get; set; } = territoryId;

    /// <summary>
    /// Account number field.
    /// </summary>
    [JsonProperty(PropertyName = "accountNumber", Order = 5)]
    public string? AccountNumber { get; set; } = accountNumber;

    /// <summary>
    /// Modified date field.
    /// </summary>
    [JsonProperty(PropertyName = "modifiedDate", Order = 6)]
    public DateTime? ModifiedDate { get; set; } = modifiedDate;

    /// <summary>
    /// Hateoas
    /// </summary>
    [JsonProperty(PropertyName = "links", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
    public IReadOnlyList<Links>? Links { get; set; } = links;

    //public CustomerDto()
    //{
    //}

    //public CustomerDto(int customerId,
    //                   int? personId,
    //                   int? storeId,
    //                   int? territoryId,
    //                   string? accountNumber,
    //                   DateTime? modifiedDate,
    //                   IReadOnlyList<Links>? links = null) =>
    //    (CustomerId, PersonId, StoreId, TerritoryId, AccountNumber, ModifiedDate, Links) =
    //    (customerId, personId, storeId, territoryId, accountNumber, modifiedDate, links);
}