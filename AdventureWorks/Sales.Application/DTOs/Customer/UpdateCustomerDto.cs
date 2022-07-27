namespace Sales.Application.DTOs.Customer;

public class UpdateCustomerDto
{
    /// <summary>
    /// Person id field.
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Store id field.
    /// </summary>
    public int? StoreId { get; set; }

    /// <summary>
    /// Territory id field.
    /// </summary>
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Account number field.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Modified date field.
    /// </summary>
    public DateTime? ModifiedDate { get; set; }
}