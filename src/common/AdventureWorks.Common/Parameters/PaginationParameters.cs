namespace AdventureWorks.Common.Parameters;

public class PaginationParameters
{
    [JsonProperty(PropertyName = "pageNumber")]
    public int PageNumber { get; set; }

    [JsonProperty(PropertyName = "pageSize")]
    public int PageSize { get; set; }

    [JsonProperty(PropertyName = "fields")]
    public string? Fields { get; set; }

    public PaginationParameters()
    {
        PageNumber = Constants.Constants.DefaultPageNumber;
        PageSize = Constants.Constants.DefaultPageSize;
        Fields = string.Empty;
    }

    public PaginationParameters(int pageNumber, int pageSize, string? fields)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Fields = fields;
    }
}