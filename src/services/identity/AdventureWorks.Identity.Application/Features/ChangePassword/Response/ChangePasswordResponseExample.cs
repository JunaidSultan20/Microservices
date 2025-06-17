namespace AdventureWorks.Identity.Application.Features.ChangePassword.Response;

/// <summary>
/// Provides examples for the ChangePasswordResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample ChangePasswordResponse instance with a successful status code and a message indicating password was changed.
/// </remarks>
public class ChangePasswordResponseExample : IExamplesProvider<ChangePasswordResponse>
{
    /// <summary>
    /// Gets a sample ChangePasswordResponse instance.
    /// </summary>
    /// <returns>A ChangePasswordResponse instance with a successful status code and a message indicating password was changed.</returns>
    public ChangePasswordResponse GetExamples() => new ChangePasswordResponse(HttpStatusCode.OK, $"{Messages.PasswordUpdatedFor} jane.doe@xyz.com");
}