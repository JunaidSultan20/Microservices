using AdventureWorks.Common.Attributes;

namespace AdventureWorks.Common.Test.Attributes;

public class RequiresParameterAttributeTest
{

    [Theory]
    [InlineData("testParam1", OpenApiParameterLocation.Query, typeof(string), true)]
    [InlineData("testParam2", OpenApiParameterLocation.Query, typeof(string), false)]
    [InlineData("testParam3", OpenApiParameterLocation.Header, typeof(string), true)]
    [InlineData("testParam4", OpenApiParameterLocation.Header, typeof(string), false)]
    public void Constructor_WhenCalled_SetsPropertiesCorrectly(string name, OpenApiParameterLocation source, Type type, bool required)
    {
        // Arrange // Act
        var attribute = new RequiresParameterAttribute
        {
            Name = name,
            Source = source,
            Type = type,
            Required = required
        };

        // Assert
        attribute.Name.Should().Be(name);
        attribute.Source.Should().Be(source);
        attribute.Type.Should().Be(type);
        attribute.Required.Should().Be(required);
    }
}