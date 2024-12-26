namespace AdventureWorks.Identity.Application.Features.Login.Response;

/// <summary>
/// Provides examples for the PostUnauthorizedAttemptResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample PostUnauthorizedAttemptResponse instance.
/// </remarks>
public class PostUnauthorizedAttemptResponseExample : IExamplesProvider<PostUnauthorizedAttemptResponse>
{
    /// <summary>
    /// Gets a sample PostUnauthorizedAttemptResponse instance.
    /// </summary>
    /// <returns>A PostUnauthorizedAttemptResponse instance.</returns>
    public PostUnauthorizedAttemptResponse GetExamples() => new PostUnauthorizedAttemptResponse();
}