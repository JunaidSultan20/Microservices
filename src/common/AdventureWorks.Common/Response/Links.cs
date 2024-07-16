namespace AdventureWorks.Common.Response;

public record Links
{
    public string? Href { get; set; }

    public string? Rel { get; }

    public string? Method { get; }

    public Links()
    {
    }

    public Links(string? href, string? rel, string? method) => (Href, Method, Rel) = (href, rel, method);

    public static Links CreateLink()
    {
        return new Links();
    }

    public Links SetLink(IUrlHelper urlHelper, 
                         string? routeName, 
                         object? routeValues, 
                         string? rel, 
                         string? method, 
                         string? scheme, 
                         string? remoteIpAddress)
    {
        Href = urlHelper.RouteUrl(routeName, routeValues)?.Replace("/api", $"{scheme}://{remoteIpAddress}/api");
        //Rel = rel;        
        //Method = method;        
        return this;
    }
}