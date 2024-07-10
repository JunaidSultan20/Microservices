namespace AdventureWorks.Common.Test.Extensions;

public class ObjectExtensionsTest
{
    [Fact]
    public void ShapeData_ShouldThrowArgumentNullException_WhenSourceIsNull()
    {
        // Arrange
        TestClass source = null;

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
        var source = new TestClass { Id = 1, Name = "Test", Description = "Test Description" };

        // Act
        var result = source.ShapeData(null);

        // Assert
        result.Should().NotBeNull();
        var expandoObject = result as IDictionary<string, object>;
        expandoObject.Should().ContainKeys("Id", "Name", "Description");
    }

    [Fact]
    public void ShapeData_ShouldReturnSpecifiedFields_WhenFieldsAreProvided()
    {
        // Arrange
        var source = new TestClass { Id = 1, Name = "Test", Description = "Test Description" };

        // Act
        var result = source.ShapeData("Id,Name");

        // Assert
        result.Should().NotBeNull();
        var expandoObject = result as IDictionary<string, object>;
        expandoObject.Should().ContainKeys("Id", "Name");
        expandoObject.Should().NotContainKeys("Description");
    }

    [Fact]
    public void ShapeData_ShouldThrowException_WhenFieldDoesNotExist()
    {
        // Arrange
        var source = new TestClass { Id = 1, Name = "Test", Description = "Test Description" };

        // Act
        Action act = () => source.ShapeData("NonExistentField");

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("Property NonExistentField wasn't found on AdventureWorks.Common.Test.Extensions.ObjectExtensionsTest+TestClass");
    }

    private class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}