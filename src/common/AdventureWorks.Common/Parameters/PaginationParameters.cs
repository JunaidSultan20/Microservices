using System.ComponentModel;
using AdventureWorks.Common.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace AdventureWorks.Common.Parameters;

/// <summary>
/// Represents the parameters for pagination in API responses.
/// </summary>
public class PaginationParameters
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    /// <example>1</example>
    [SwaggerSchema("Page number of results to fetch.")]
    [DefaultValue(1)]
    [Required]
    [JsonProperty(PropertyName = "pageNumber")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    /// <example>10</example>
    [SwaggerSchema("Page size of results to fetch.")]
    [DefaultValue(10)]
    [Required]
    [JsonProperty(PropertyName = "pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the fields to include in the response.
    /// </summary>
    /// <example>id, name</example>
    [SwaggerSchema("Fields that need to be the part of the result set")]
    [DefaultValue("customerId, accountNumber")]
    [JsonProperty(PropertyName = "fields")]
    public string? Fields { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationParameters"/> class with default values.
    /// </summary>
    public PaginationParameters() => (PageNumber, PageSize, Fields) = (Constants.Constants.DefaultPageNumber, Constants.Constants.DefaultPageSize, string.Empty);

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationParameters"/> class with specified values.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="fields">The fields to include in the response.</param>
    public PaginationParameters(int pageNumber, int pageSize, string? fields) => (PageNumber, PageSize, Fields) = (pageNumber, pageSize, fields);
}