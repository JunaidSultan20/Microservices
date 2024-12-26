namespace AdventureWorks.Common.Attributes;

/// <summary>
/// Specifies that the decorated method requires a specific parameter to be present.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class RequiresParameterAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the name of the required parameter.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the location of the parameter (e.g., Query, Header, Path).
    /// </summary>
    public OpenApiParameterLocation Source { get; set; }

    /// <summary>
    /// Gets or sets the type of the required parameter.
    /// </summary>
    public required Type Type { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the parameter is required.
    /// </summary>
    public required bool Required { get; set; }
}