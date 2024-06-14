using AdventureWorks.Common.Exceptions;
using AdventureWorks.Common.Helpers;

namespace AdventureWorks.Common.Test.Exceptions;

public class ApiExceptionTest
{
    [Fact]
    public void Constructor_WithMessage_SetsMessageAndGeneratesId()
    {
        // Arrange // Act
        string exceptionMessage = "Test message";

        var exception = new ApiException(exceptionMessage);

        var id = HelperMethods.GetInternalProperty<Guid?>(exception, "Id");
        var message = HelperMethods.GetInternalProperty<string?>(exception, "Message");
        var details = HelperMethods.GetInternalProperty<string?>(exception, "Details");
        var innerException = HelperMethods.GetInternalProperty<string?>(exception, "InnerException");
        var stackTrace = HelperMethods.GetInternalProperty<string?>(exception, "StackTrace");

        // Assert
        id.Should().NotBeNull();
        message.Should().Be(exceptionMessage);
        details.Should().BeNull();
        innerException.Should().BeNull();
        stackTrace.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithAllParameters_SetsPropertiesCorrectly()
    {
        // Arrange // Act
        Guid id = Guid.NewGuid();
        string message = "Test message";
        string details = "Test details";
        string innerException = "Test inner exception";
        string stackTrace = "Test stack trace";

        var exception = new ApiException(id, message, details, innerException, stackTrace);

        var idProperty = HelperMethods.GetInternalProperty<Guid?>(exception, "Id");
        var messageProperty = HelperMethods.GetInternalProperty<string?>(exception, "Message");
        var detailsProperty = HelperMethods.GetInternalProperty<string?>(exception, "Details");
        var innerExceptionProperty = HelperMethods.GetInternalProperty<string?>(exception, "InnerException");
        var stackTraceProperty = HelperMethods.GetInternalProperty<string?>(exception, "StackTrace");

        // Assert
        idProperty.Should().Be(id);
        messageProperty.Should().Be(message);
        detailsProperty.Should().Be(details);
        innerExceptionProperty.Should().Be(innerException);
        stackTraceProperty.Should().Be(stackTrace);
    }

    [Fact]
    public void Constructor_WithAllParametersAndNullId_GeneratesId()
    {
        // Arrange // Act
        string message = "Test message";
        string details = "Test details";
        string innerException = "Test inner exception";
        string stackTrace = "Test stack trace";

        var exception = new ApiException(null, message, details, innerException, stackTrace);

        var idProperty = HelperMethods.GetInternalProperty<Guid?>(exception, "Id");
        var messageProperty = HelperMethods.GetInternalProperty<string?>(exception, "Message");
        var detailsProperty = HelperMethods.GetInternalProperty<string?>(exception, "Details");
        var innerExceptionProperty = HelperMethods.GetInternalProperty<string?>(exception, "InnerException");
        var stackTraceProperty = HelperMethods.GetInternalProperty<string?>(exception, "StackTrace");

        // Assert
        idProperty.Should().BeNull();
        messageProperty.Should().Be(message);
        detailsProperty.Should().Be(details);
        innerExceptionProperty.Should().Be(innerException);
        stackTraceProperty.Should().Be(stackTrace);
    }
}