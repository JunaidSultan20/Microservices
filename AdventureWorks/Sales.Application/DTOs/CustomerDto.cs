namespace Sales.Application.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public int? StoreId { get; set; }
    public int? TerritoryId { get; set; }
    public string? AccountNumber { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

public class CustomerWithLinksDto
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public int? StoreId { get; set; }
    public int? TerritoryId { get; set; }
    public string? AccountNumber { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public IReadOnlyList<Links>? Links { get; set; } = null;
}