namespace Sales.Application.DTOs.Customer;

public class CreateCustomerDto
{
    /// <summary>
    /// PersonId field
    /// </summary>
    public string? PersonId { get; set; }

    /// <summary>
    /// StoreId field
    /// </summary>
    public string? StoreId { get; set; }

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
}