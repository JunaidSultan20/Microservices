namespace AdventureWorks.Identity.Application.Features.Login.Request;

/// <summary>
/// Provides examples for the PostLoginRequest class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample PostLoginRequest instance with pre-populated authentication information.
/// </remarks>
public class PostLoginRequestExample : IExamplesProvider<PostLoginRequest>
{
    /// <summary>
    /// Gets a sample PostLoginRequest instance with username and password.
    /// </summary>
    /// <returns>A PostLoginRequest instance with pre-populated authentication information (for example purposes).</returns>
    public PostLoginRequest GetExamples() => new PostLoginRequest(new AuthenticationDto("jane.doe@example.com", "janeDoe@123"));
}