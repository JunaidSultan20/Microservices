namespace AdventureWorks.Common.Test.Extensions;

public class EnumerableExtensionsTest
{
    [Fact]
    public void ShapeData_ShouldReturnAllProperties_WhenFieldsAreNullOrEmpty()
    {
        // Arrange
        var testData = new List<TestClass>
        {
            new TestClass { Id = 1, Name = "Name1", Description = "Description1" },
            new TestClass { Id = 2, Name = "Name2", Description = "Description2" }
        };

        // Act
        var result = testData.ShapeData(null).ToList();

        // Assert
        result.Should().HaveCount(2);
        result[0].Should().ContainKey("Id").WhoseValue.Should().Be(1);
        result[0].Should().ContainKey("Name").WhoseValue.Should().Be("Name1");
        result[0].Should().ContainKey("Description").WhoseValue.Should().Be("Description1");
    }

    [Fact]
    public void ShapeData_ShouldReturnSpecificProperties_WhenFieldsAreSpecified()
    {
        // Arrange
        var testData = new List<TestClass>
        {
            new TestClass { Id = 1, Name = "Name1", Description = "Description1" },
            new TestClass { Id = 2, Name = "Name2", Description = "Description2" }
        };

        // Act
        var result = testData.ShapeData("Id, Name").ToList();

        // Assert
        result.Should().HaveCount(2);
        result[0].Should().ContainKey("Id").WhoseValue.Should().Be(1);
        result[0].Should().ContainKey("Name").WhoseValue.Should().Be("Name1");
        result[0].Should().NotContainKey("Description");
    }

    [Fact]
    public void ShapeData_ShouldThrowException_WhenInvalidFieldIsSpecified()
    {
        // Arrange
        var testData = new List<TestClass>
        {
            new TestClass { Id = 1, Name = "Name1", Description = "Description1" }
        };

        // Act
        Action act = () => testData.ShapeData("InvalidField").ToList();

        // Assert
        act.Should().Throw<Exception>().WithMessage("Property InvalidField wasn't found on *");
    }

    [Fact]
    public void ShapeData_ShouldThrowArgumentNullException_WhenSourceIsNull()
    {
        // Arrange
        List<TestClass>? testData = null;

        // Act
        Action act = () => testData.ShapeData(null).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*source*");
    }

    private class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

