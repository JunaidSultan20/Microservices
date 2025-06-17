using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Application.Features.ChangePassword.Response;

/// <summary>
/// Provides examples for the ChangePasswordUnauthorizedResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample ChangePasswordUnauthorizedResponse instance.
/// </remarks>
public class ChangePasswordUnauthorizedResponseExample : IExamplesProvider<ChangePasswordUnauthorizedResponse>
{
    /// <summary>
    /// Gets a sample ChangePasswordUnauthorizedResponse instance.
    /// </summary>
    /// <returns>A ChangePasswordUnauthorizedResponse instance.</returns>
    public ChangePasswordUnauthorizedResponse GetExamples() => new ChangePasswordUnauthorizedResponse();
}