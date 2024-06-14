namespace AdventureWorks.Common.Attributes;

/// <summary>
/// Specifies that a method requires a specific parameter with defined attributes.
/// </summary>
/// <param name="Name" example="id">The name of the required parameter.</param>
/// <param name="Source" example="OpenApiParameterLocation.Route">The location from where the parameter value is sourced.</param>
/// <param name="Type" example="int">The type of the required parameter.</param>
/// <param name="Required" example="true">Indicates whether the parameter is required.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class RequiresParameterAttribute : Attribute
{
    public required string Name { get; set; }

    public OpenApiParameterLocation Source { get; set; }

    public required Type Type { get; set; }

    public required bool Required { get; set; }
}