namespace AdventureWorks.Common.Test.Extensions;

public class ListExtensionsTest
{
    [Fact]
    public void ShapeData_ShouldThrowArgumentNullException_WhenSourceIsNull()
    {
        // Arrange
        List<TestClass> source = null;

        // Act
        Action act = () => source.ShapeData(null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'source')");
    }

    [Fact]
    public void ShapeData_ShouldReturnAllFields_WhenFieldsIsNullOrWhiteSpace()
    {
        // Arrange
        var source = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "Test", Description = "Test Description" }
            };

        // Act
        var result = source.ShapeData(null);

        // Assert
        result.Should().HaveCount(1);
        var expandoObject = result.First() as IDictionary<string, object>;
        expandoObject.Should().ContainKeys("Id", "Name", "Description");
    }

    [Fact]
    public void ShapeData_ShouldReturnSpecifiedFields_WhenFieldsAreProvided()
    {
        // Arrange
        var source = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "Test", Description = "Test Description" }
            };

        // Act
        var result = source.ShapeData("Id,Name");

        // Assert
        result.Should().HaveCount(1);
        var expandoObject = result.First() as IDictionary<string, object>;
        expandoObject.Should().ContainKeys("Id", "Name");
        expandoObject.Should().NotContainKeys("Description");
    }

    [Fact]
    public void ShapeData_ShouldThrowException_WhenFieldDoesNotExist()
    {
        // Arrange
        var source = new List<TestClass>
        {
            new TestClass { Id = 1, Name = "Test", Description = "Test Description" }
        };

        // Act
        Action act = () => source.ShapeData("NonExistentField");

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("Property NonExistentField wasn't found on AdventureWorks.Common.Test.Extensions.ListExtensionsTest+TestClass");
    }

    [Fact]
    public void ShapeData_ShouldHandleEmptySourceList()
    {
        // Arrange
        var source = new List<TestClass>();

        // Act
        var result = source.ShapeData("Id,Name");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ShapeData_ShouldHandleEmptyFields()
    {
        // Arrange
        var source = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "Test", Description = "Test Description" }
            };

        // Act
        var result = source.ShapeData(string.Empty);

        // Assert
        result.Should().HaveCount(1);
        var expandoObject = result.First() as IDictionary<string, object>;
        expandoObject.Should().ContainKeys("Id", "Name", "Description");
    }

    private class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}