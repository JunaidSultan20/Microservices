using System.Net;
using AdventureWorks.Common.Constants;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.Common.Helpers;
using AdventureWorks.Common.Response;

namespace AdventureWorks.Common.Test.Exceptions;

public class ValidationExceptionTest
{
    [Fact]
    public void Constructor_WithErrors_SetsErrorsCorrectly()
    {
        // Arrange
        var errors = new List<ValidationError>
        {
            new ValidationError("Field1", "Error message 1"),
            new ValidationError("Field2", "Error message 2")
        };

        // Act
        var exception = new ValidationException(errors);

        // Assert
        exception.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        
        //errorsProperty.Should().BeEquivalentTo(errors);
        exception.Errors.Should().BeEquivalentTo(errors);

        // Ensure the message matches the expected value
        exception.Message.Should().Be(Messages.ValidationError);
    }
}