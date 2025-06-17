namespace AdventureWorks.Identity.Application.Features.ChangePassword.Request;

/// <summary>
/// Provides examples for the ChangePasswordRequest class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample ChangePasswordRequest instance with pre-populated password information.
/// </remarks>
public class ChangePasswordRequestExample : IExamplesProvider<ChangePasswordRequest>
{
    /// <summary>
    /// Gets a sample PostLoginRequest instance with username and password.
    /// </summary>
    /// <returns>A PostLoginRequest instance with pre-populated authentication information (for example purposes).</returns>
    public ChangePasswordRequest GetExamples() => new ChangePasswordRequest(new ChangePasswordDto("jane.doe@xyz.com", "janedoe@123", "janeDoe@123"));
}