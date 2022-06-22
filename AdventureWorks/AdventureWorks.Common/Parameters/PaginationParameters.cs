namespace AdventureWorks.Common.Parameters;

public class PaginationParameters
{
    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }

    public string? Fields { get; set; }

    public PaginationParameters()
    {
        PageNumber = 1;
        PageSize = 10;
        Fields = string.Empty;
    }

    public PaginationParameters(int pageNumber, int pageSize, string fields)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
        Fields = fields;
    }
}