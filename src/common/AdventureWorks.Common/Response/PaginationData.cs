namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents pagination information for a data set.
/// </summary>
public record PaginationData
{
    /// <summary>
    /// Gets or sets the total number of records.
    /// </summary>
    /// <example>200</example>
    public int? TotalRecords { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    /// <example>1</example>
    public int? CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the number of records per page.
    /// </summary>
    /// <example>20</example>
    public int? PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages.
    /// </summary>
    /// <example>10</example>
    public int? TotalPages { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether there are previous pages.
    /// </summary>
    /// <example>true</example>
    public bool? HasPrevious { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether there are next pages.
    /// </summary>
    /// <example>true</example>
    public bool? HasNext { get; set; }

    /// <summary>
    /// Gets or sets the URL for the previous page.
    /// </summary>
    /// <example>https://example.com/data?pageNumber=1&pageSize=20</example>
    public string? PreviousPageLink { get; set; }

    /// <summary>
    /// Gets or sets the URL for the next page.
    /// </summary>
    /// <example>https://example.com/data?pageNumber=3&pageSize=20</example>
    public string? NextPageLink { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationData"/> class with default values.
    /// </summary>
    public PaginationData() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationData"/> class with specified total records and page size.
    /// </summary>
    /// <param name="totalRecords">The total number of records.</param>
    /// <param name="pageSize">The number of records per page.</param>
    public PaginationData(int totalRecords, int pageSize)
    {
        TotalRecords = totalRecords;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationData"/> class with specified total records, page number, and page size.
    /// </summary>
    /// <param name="totalRecords">The total number of records.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of records per page.</param>
    public PaginationData(int totalRecords, int pageNumber, int pageSize) : this(totalRecords, pageSize)
    {
        CurrentPage = pageNumber;
        HasPrevious = CurrentPage > 1;
        HasNext = CurrentPage < TotalPages;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationData"/> class with specified total records, page number, page size, and base URL.
    /// </summary>
    /// <param name="totalRecords">The total number of records.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of records per page.</param>
    /// <param name="url">The base URL for pagination links.</param>
    public PaginationData(int totalRecords, int pageNumber, int pageSize, string url) : this(totalRecords, pageNumber, pageSize)
    {
        PreviousPageLink = pageNumber > 1 ? $"{url}?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null;
        NextPageLink = pageNumber != TotalPages ? $"{url}?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null;
    }
}