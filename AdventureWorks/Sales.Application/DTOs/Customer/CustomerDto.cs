namespace Sales.Application.DTOs.Customer;

/// <summary>
/// Customer Dto class that returns customer details.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Customer id field.
    /// </summary>
    public int Id { get; set; }

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