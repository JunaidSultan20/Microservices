namespace AdventureWorks.Common.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class RequiresParameterAttribute : Attribute
{
    public required string Name { get; set; }

    public OpenApiParameterLocation Source { get; set; }

    public required Type Type { get; set; }

    public required bool Required { get; set; }
}