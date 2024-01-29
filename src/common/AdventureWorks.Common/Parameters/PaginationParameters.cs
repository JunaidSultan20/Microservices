namespace AdventureWorks.Common.Parameters;

/// <summary>
/// Represents the parameters for paginating data.
/// </summary>
public class PaginationParameters
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    /// <example>1</example>
    [JsonProperty(PropertyName = "pageNumber")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    /// <example>10</example>
    [JsonProperty(PropertyName = "pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the fields to include in the response.
    /// </summary>
    /// <example>id, name</example>
    [JsonProperty(PropertyName = "fields")]
    public string? Fields { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationParameters"/> class with default values.
    /// </summary>
    public PaginationParameters()
    {
        PageNumber = Constants.Constants.DefaultPageNumber;
        PageSize = Constants.Constants.DefaultPageSize;
        Fields = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationParameters"/> class with specified values.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="fields">The fields to include in the response.</param>
    public PaginationParameters(int pageNumber, int pageSize, string? fields)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Fields = fields;
    }
}