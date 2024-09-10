namespace AdventureWorks.Sales.Customers.Dto;

public class CreateCustomerDto
{
    [JsonProperty(PropertyName = "customerId")]
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

    [JsonProperty(PropertyName = "rowguid")]
    public string Rowguid { get; set; }

    [JsonProperty(PropertyName = "modifiedDate")]
    public DateTime ModifiedDate { get; set; }
}