using AdventureWorks.Contracts.EventStreaming;
using AdventureWorks.Events.Streams;
using AdventureWorks.Identity.Application.DomainEvents.Roles;
using AdventureWorks.Identity.Application.Features.PostRole.Request;
using AdventureWorks.Identity.Application.Features.PostRole.Response;

namespace AdventureWorks.Identity.Application.Features.PostRole.Handler;

public class PostRoleHandler(RoleManager<Role> roleManager, RoleAggregate roleAggregate, IEventStore eventStore) : IRequestHandler<PostRoleRequest, PostRoleResponse> 
{
    public async Task<PostRoleResponse> Handle(PostRoleRequest request, CancellationToken cancellationToken = default)
    {
        Role? role = await roleManager.FindByNameAsync(request.CreateRoleDto.Name);
        if (role is not null)
            return new ConflictPostRoleResponse();

        role = new Role(request.CreateRoleDto.Name, request.CreateRoleDto.Name.ToUpper());
        IdentityResult result = await roleManager.CreateAsync(role);

        if (!result.Succeeded)
            return new BadRequestPostRoleResponse(Messages.UnableToCreateRole);

        roleAggregate.RoleCreatedEvent(request.CreateRoleDto.Name);
        await eventStore.SaveAsync(roleAggregate, role.Id.ToString(), IdentityStreams.RoleStream);

        return new PostRoleResponse(HttpStatusCode.Created, Messages.RoleCreated);
    }
}