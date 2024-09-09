using AdventureWorks.Identity.Application.Features.PostRole.Response;

namespace AdventureWorks.Identity.Application.Features.PostRole.Request;

public class PostRoleRequest(CreateRoleDto createRoleDto) : IRequest<PostRoleResponse>
{
    public CreateRoleDto CreateRoleDto { get; set; } = createRoleDto;
}