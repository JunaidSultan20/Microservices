namespace Sales.Application.DTOs.Customer;

public class CustomerWithLinksDto
{
    /// <summary>
    /// Id field
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// PersonId field
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// StoreId field
    /// </summary>
    public int? StoreId { get; set; }

    /// <summary>
    /// TerritoryId field
    /// </summary>
    public int? TerritoryId { get; set; }

    /// <summary>
    /// AccountNumber field
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// ModifiedDate field
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Hateoas links field
    /// </summary>
    public IReadOnlyList<Links>? Links { get; set; } = null;
}