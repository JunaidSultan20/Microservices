namespace AdventureWorks.Common.Constants;

public static class Messages
{
    //Success Messages
    public const string BearerTokenGenerated = "Bearer token generated";
    public const string PasswordUpdatedFor = "Password update successfully for";
    public const string LogoutSuccessful = "User logged out successfully";
    public const string BearerTokenRefreshed = "Bearer token refreshed successfully";
    public const string RecordsRetrievedSuccessfully = "Records retrieved successfully";
    public const string RecordDeleted = "Record deleted successfully";

    //Error Messages
    public const string InvalidUsername = "Invalid username";
    public const string UnableToChangePassword = "Unable to change password";
    public const string UnauthorizedAttempt = "Unauthorized attempt";
    public const string LogoutFailed = "Unable to logout user";
    public const string InvalidTokenModel = "Invalid token model";
    public const string UserExists = "User already exists";
    public const string UnableToCreateUser = "Unable to create user";
    public const string UnableToAssignRole = "Unable to assign role to user";
    public const string UserCreatedSuccessfully = "User created successfully";
    public const string UserCreatedFailed = "Unable to create new user";
    public const string UserNotFound = "User not found";
    public const string RoleNotFound = "No role exists against the specified role in request";
    public const string InvalidAccessToken = "Invalid access token provided";
    public const string RefreshTokenExpired = "Refresh token has expired";
    public const string RefreshTokenNotFound = "Refresh token not found";
    public const string ValidationError = "One or more validation error occurred";
    public const string InvalidMediaType = "Invalid media type provided";
    public const string BadRequest = "Bad request";
    public const string NotFoundById = "No record found against the id specified";
    public const string UnableToCreateCustomer = "Unable to create customer";
    public const string UnableToDeleteRecord = "Unable to delete record";

    //Exception Messages
    public const string ArgumentNullExceptionMessage = "Argument null exception";
}