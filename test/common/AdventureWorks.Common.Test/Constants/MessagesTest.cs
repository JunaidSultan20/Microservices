using AdventureWorks.Common.Constants;

namespace AdventureWorks.Common.Test.Constants;

public class MessagesTest
{
    [Fact]
    public void Messages_ShouldHaveCorrectValues()
    {
        // Assert
        Messages.BearerTokenGenerated.Should().Be("Bearer token generated");
        Messages.BearerTokenGenerated.Should().BeOfType(typeof(string));
        
        Messages.PasswordUpdatedFor.Should().Be("Password update successfully for");
        Messages.PasswordUpdatedFor.Should().BeOfType(typeof(string));

        Messages.LogoutSuccessful.Should().Be("User logged out successfully");
        Messages.LogoutSuccessful.Should().BeOfType(typeof(string));
        
        Messages.BearerTokenRefreshed.Should().Be("Bearer token refreshed successfully");
        Messages.BearerTokenRefreshed.Should().BeOfType(typeof(string));
        
        Messages.RecordsRetrievedSuccessfully.Should().Be("Records retrieved successfully");
        Messages.RecordsRetrievedSuccessfully.Should().BeOfType(typeof(string));

        Messages.RecordDeleted.Should().Be("Record deleted successfully");
        Messages.RecordDeleted.Should().BeOfType(typeof(string));
        
        Messages.InvalidUsername.Should().Be("Invalid username");
        Messages.InvalidUsername.Should().BeOfType(typeof(string));
        
        Messages.UnableToChangePassword.Should().Be("Unable to change password");
        Messages.UnableToChangePassword.Should().BeOfType(typeof(string));

        Messages.UnauthorizedAttempt.Should().Be("Unauthorized attempt");
        Messages.UnauthorizedAttempt.Should().BeOfType(typeof(string));

        Messages.LogoutFailed.Should().Be("Unable to logout user");
        Messages.LogoutFailed.Should().BeOfType(typeof(string));

        Messages.InvalidTokenModel.Should().Be("Invalid token model");
        Messages.InvalidTokenModel.Should().BeOfType(typeof(string));

        Messages.UserExists.Should().Be("User already exists");
        Messages.UserExists.Should().BeOfType(typeof(string));

        Messages.UnableToCreateUser.Should().Be("Unable to create user");
        Messages.UnableToCreateUser.Should().BeOfType(typeof(string));

        Messages.UnableToAssignRole.Should().Be("Unable to assign role to user");
        Messages.UnableToAssignRole.Should().BeOfType(typeof(string));

        Messages.UserCreatedSuccessfully.Should().Be("User created successfully");
        Messages.UserCreatedSuccessfully.Should().BeOfType(typeof(string));

        Messages.UserCreatedFailed.Should().Be("Unable to create new user");
        Messages.UserCreatedFailed.Should().BeOfType(typeof(string));

        Messages.UserNotFound.Should().Be("User not found");
        Messages.UserNotFound.Should().BeOfType(typeof(string));

        Messages.InvalidAccessToken.Should().Be("Invalid access token provided");
        Messages.InvalidAccessToken.Should().BeOfType(typeof(string));

        Messages.RefreshTokenExpired.Should().Be("Refresh token has expired");
        Messages.RefreshTokenExpired.Should().BeOfType(typeof(string));

        Messages.RefreshTokenNotFound.Should().Be("Refresh token not found");
        Messages.RefreshTokenNotFound.Should().BeOfType(typeof(string));

        Messages.ValidationError.Should().Be("One or more validation error occurred");
        Messages.ValidationError.Should().BeOfType(typeof(string));

        Messages.InvalidMediaType.Should().Be("Invalid media type provided");
        Messages.InvalidMediaType.Should().BeOfType(typeof(string));

        Messages.BadRequest.Should().Be("Bad request");
        Messages.BadRequest.Should().BeOfType(typeof(string));

        Messages.NotFoundById.Should().Be("No record found against the id specified");
        Messages.NotFoundById.Should().BeOfType(typeof(string));

        Messages.UnableToCreateCustomer.Should().Be("Unable to create customer");
        Messages.UnableToCreateCustomer.Should().BeOfType(typeof(string));

        Messages.UnableToDeleteRecord.Should().Be("Unable to delete record");
        Messages.UnableToDeleteRecord.Should().BeOfType(typeof(string));

        Messages.ArgumentNullExceptionMessage.Should().Be("Argument null exception");
        Messages.ArgumentNullExceptionMessage.Should().BeOfType(typeof(string));
    }
}