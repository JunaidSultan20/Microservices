using AdventureWorks.Identity.Application.Features.PostRole.Response;

namespace AdventureWorks.Identity.Application.Features.PostRole.Request;

/// <summary>
/// Represents the request for creating a new role.
/// </summary>
/// <remarks>
/// This class implements the IRequest interface and defines the request for a post-role operation.
/// It includes a CreateRoleDto property to hold the role creation information.
/// </remarks>
/// <param name="createRoleDto">The role creation information for the post-role request.</param>
public class PostRoleRequest(CreateRoleDto createRoleDto) : IRequest<PostRoleResponse>
{
    /// <summary>
    /// Gets or sets the role creation information for the post-role request.
    /// </summary>
    public CreateRoleDto CreateRoleDto { get; set; } = createRoleDto;
}