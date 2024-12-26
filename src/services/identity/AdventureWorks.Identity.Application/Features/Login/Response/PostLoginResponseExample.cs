namespace AdventureWorks.Identity.Application.Features.Login.Response;

/// <summary>
/// Provides examples for the PostLoginResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample PostLoginResponse instance with a successful status code and a message indicating a generated bearer token.
/// </remarks>
public class PostLoginResponseExample : IExamplesProvider<PostLoginResponse>
{
    /// <summary>
    /// Gets a sample PostLoginResponse instance.
    /// </summary>
    /// <returns>A PostLoginResponse instance with a successful status code and a message indicating a generated bearer token.</returns>
    public PostLoginResponse GetExamples() => new PostLoginResponse(HttpStatusCode.OK, Messages.BearerTokenGenerated);
}