using AdventureWorks.Common.Response;

namespace AdventureWorks.Common.Helpers;

public static class LinksHelper
{
    public static IReadOnlyList<Links> CreateLinks(object id, string? fields, IUrlService urlService)
    {
        List<Links> links = new List<Links>();

        if (!string.IsNullOrWhiteSpace(fields))
        {
            links.Add(new Links(urlService.GetCurrentRequestUrl(), "self", "GET"));
        }
        else
        {
            links.Add(new Links(urlService.GetCurrentRequestUrl(), "self", "GET"));
        }

        IReadOnlyList<Links> readOnlyList = links;
        return readOnlyList;
    }
}