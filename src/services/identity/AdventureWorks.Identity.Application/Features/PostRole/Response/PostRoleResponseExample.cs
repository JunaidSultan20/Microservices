namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

/// <summary>
/// Provides examples for the PostRoleResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample PostRoleResponse instance with a successful status code and a message indicating the role creation.
/// </remarks>
public class PostRoleResponseExample : IExamplesProvider<PostRoleResponse>
{
    /// <summary>
    /// Gets a sample PostRoleResponse instance.
    /// </summary>
    /// <returns>A PostRoleResponse instance with a successful status code and a message indicating the role creation.</returns>
    public PostRoleResponse GetExamples()
    {
        return new PostRoleResponse(HttpStatusCode.Created, Messages.RoleCreated);
    }
}