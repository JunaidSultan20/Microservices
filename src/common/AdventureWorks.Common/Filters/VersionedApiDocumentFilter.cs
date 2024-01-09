namespace AdventureWorks.Common.Filters;

public class VersionedApiDocumentFilter : IDocumentFilter
{
    private readonly ApiVersion _version;

    public VersionedApiDocumentFilter(ApiVersion version)
    {
        _version = version;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Remove paths that should be excluded for the specified version
        var excludedPaths = context.ApiDescriptions
                                   .Where(api => api.RelativePath.Contains("CreateCustomer"))
                                   .Where(api => api.GetApiVersion().ToString() != _version.ToString())
                                   .Select(api => "/" + api.RelativePath.Trim('/'))
                                   .ToList();

        foreach (var excludedPath in excludedPaths)
        {
            swaggerDoc.Paths.Remove(excludedPath);
        }
    }
}