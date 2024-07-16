namespace AdventureWorks.Common.Test.Extensions;

public class ArgumentCheckExtensionsTest
{
    [Fact]
    public void IsNotNull_ShouldReturnObject_WhenObjectIsNotNull()
    {
        // Arrange
        var testObject = new object();

        // Act
        var result = testObject.IsNotNull();

        // Assert
        result.Should().BeSameAs(testObject);
    }

    [Fact]
    public void IsNotNull_ShouldThrowArgumentNullException_WhenObjectIsNull()
    {
        // Arrange
        object? testObject = null;

        // Act
        Action act = () => testObject.IsNotNull();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IsNotNull_ShouldThrowArgumentNullExceptionWithCorrectParamName_WhenObjectIsNull()
    {
        // Arrange
        object? testObject = null;

        // Act
        Action act = () => testObject.IsNotNull();

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .And.ParamName.Should().Be(nameof(testObject));
    }

    [Fact]
    public void IsNotNull_ShouldThrowArgumentNullExceptionWithCustomParamName_WhenObjectIsNull()
    {
        // Arrange
        object? testObject = null;
        string customParamName = "customParam";

        // Act
        Action act = () => testObject.IsNotNull(customParamName);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .And.ParamName.Should().Be(customParamName);
    }
}