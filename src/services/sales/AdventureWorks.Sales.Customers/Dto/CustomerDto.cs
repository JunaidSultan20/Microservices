namespace AdventureWorks.Sales.Customers.Dto;

/// <summary>
/// Customer Dto class that returns customer details.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Customer id field.
    /// </summary>
    [JsonProperty(PropertyName = "id", Order = 1)]
    public int CustomerId { get; set; }

    /// <summary>
    /// Person id field.
    /// </summary>
    [JsonProperty(PropertyName = "personId", Order = 2)]
    public int? PersonId { get; set; }

    /// <summary>
    /// Store id field.
    /// </summary>
    [JsonProperty(PropertyName = "storeId", Order = 3)]
    public int? StoreId { get; set; }

    /// <summary>
    /// Territory id field.
    /// </summary>
    [JsonProperty(PropertyName = "territoryId", Order = 4)]
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Account number field.
    /// </summary>
    [JsonProperty(PropertyName = "accountNumber", Order = 5)]
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Modified date field.
    /// </summary>
    [JsonProperty(PropertyName = "modifiedDate", Order = 6)]
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Hateoas
    /// </summary>
    [JsonProperty(PropertyName = "links", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
    public IReadOnlyList<Links>? Links { get; set; }

    public CustomerDto()
    {
    }

    public CustomerDto(int id,
                       int? personId,
                       int? storeId,
                       int? territoryId,
                       string? accountNumber,
                       DateTime? modifiedDate,
                       IReadOnlyList<Links>? links = null) =>
                      (CustomerId, PersonId, StoreId, TerritoryId, AccountNumber, ModifiedDate, Links) =
                      (id, personId, storeId, territoryId, accountNumber, modifiedDate, links);
}