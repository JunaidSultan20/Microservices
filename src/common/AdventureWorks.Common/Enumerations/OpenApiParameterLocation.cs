namespace AdventureWorks.Common.Enumerations;

/// <summary>
/// Specifies the location of a parameter in an OpenAPI operation.
/// </summary>
public enum OpenApiParameterLocation
{
    Query = 1,
    Header,
    Route,
    Cookie,
    Body
}