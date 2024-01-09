namespace AdventureWorks.Common.Response;

public class Links
{
    public string? Href { get; set; }

    public string? Rel { get; }

    public string? Method { get; }

    public Links() { }

    public Links(string? href, string? rel, string? method) => (Href, Method, Rel) = (href, rel, method);
}