namespace Sales.Application.DTOs.Base;

public class LinksDto
{
    public string? Href { get; set; }

    public string? Rel { get; private set; }

    public string? Method { get; private set; }

    public LinksDto(string? href, string? rel, string? method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}