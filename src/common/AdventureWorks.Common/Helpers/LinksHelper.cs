namespace AdventureWorks.Common.Helpers;

public static class LinksHelper
{
    public static IReadOnlyList<Links> CreateLinks(object id, string? fields, IUrlService urlService)
    {
        List<Links> links = new List<Links>();

        if (!string.IsNullOrWhiteSpace(value: fields))
        {
            links.Add(new Links(href: urlService.GetCurrentRequestUrl(), rel: "self", method: "GET"));
        }
        else
        {
            links.Add(new Links(href: urlService.GetCurrentRequestUrl(), rel: "self", method: "GET"));
        }

        IReadOnlyList<Links> readOnlyList = links;

        return readOnlyList;
    }
}