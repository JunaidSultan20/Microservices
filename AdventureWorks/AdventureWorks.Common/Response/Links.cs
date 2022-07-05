namespace AdventureWorks.Common.Response;

public class Links
{
    public string? Href { get; set; }

    public string? Rel { get; private set; }

    public string? Method { get; private set; }

    public Links(string? href, string? rel, string? method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}