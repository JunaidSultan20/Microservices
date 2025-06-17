namespace AdventureWorks.Identity.Application.Features.ChangePassword.Response;

/// <summary>
/// Provides examples for the BadRequestChangePasswordResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample BadRequestChangePasswordResponse instance with a bad request code and a message indicating the error.
/// </remarks>
public class BadRequestChangePasswordResponseExample : IExamplesProvider<BadRequestChangePasswordResponse>
{
    /// <summary>
    /// Gets a sample BadRequestChangePasswordResponse instance.
    /// </summary>
    /// <returns>A BadRequestChangePasswordResponse instance with a bad request and a message indicating the error.</returns>
    public BadRequestChangePasswordResponse GetExamples() => new BadRequestChangePasswordResponse();
}