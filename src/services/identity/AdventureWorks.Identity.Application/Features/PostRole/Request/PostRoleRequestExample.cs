namespace AdventureWorks.Identity.Application.Features.PostRole.Request;

/// <summary>
/// Provides examples for the PostRoleRequest class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample PostRoleRequest instance with a pre-populated CreateRoleDto.
/// </remarks>
public class PostRoleRequestExample : IExamplesProvider<PostRoleRequest>
{
    /// <summary>
    /// Gets a sample PostRoleRequest instance with a pre-defined role name.
    /// </summary>
    /// <returns>A PostRoleRequest instance with a CreateRoleDto containing the role name "Guest".</returns>
    public PostRoleRequest GetExamples()
    {
        return new PostRoleRequest(new CreateRoleDto(Name: "Guest"));
    }
}