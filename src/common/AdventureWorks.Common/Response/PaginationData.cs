namespace AdventureWorks.Common.Response;

public record PaginationData
{
    public int? TotalRecords { get; set; }

    public int? CurrentPage { get; set; }

    public int? PageSize { get; set; }

    public int? TotalPages { get; set; }

    public bool? HasPrevious { get; set; }

    public bool? HasNext { get; set; }

    public string? PreviousPageLink { get; set; }

    public string? NextPageLink { get; set; }

    public PaginationData() { }

    public PaginationData(int totalRecords, int pageSize)
    {
        TotalRecords = totalRecords;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }

    public PaginationData(int? totalRecords, int? pageNumber, int? pageSize)
    {
        TotalRecords = totalRecords;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        if (totalRecords is not null && pageSize is not null)
            TotalPages = (int)Math.Ceiling(totalRecords.Value / (double)pageSize.Value);
        HasPrevious = CurrentPage > 1;
        HasNext = CurrentPage < TotalPages;
    }

    public PaginationData(int? totalRecords, int? pageNumber, int? pageSize, string url)
    {
        TotalRecords = totalRecords;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        if (totalRecords is not null && pageSize is not null)
            TotalPages = (int)Math.Ceiling(totalRecords.Value / (double)pageSize.Value);
        HasPrevious = CurrentPage > 1;
        HasNext = CurrentPage < TotalPages;
        PreviousPageLink = pageNumber > 1 ? $"{url}?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null;
        NextPageLink = pageNumber != TotalPages ? $"{url}?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null;
    }
}