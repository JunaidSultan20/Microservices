using AdventureWorks.Contracts.EventStreaming;
using AdventureWorks.Events.Streams;
using AdventureWorks.Identity.Application.DomainEvents.Roles;
using AdventureWorks.Identity.Application.Features.PostRole.Request;
using AdventureWorks.Identity.Application.Features.PostRole.Response;

namespace AdventureWorks.Identity.Application.Features.PostRole.Handler;

/// <summary>
/// Handles post-role requests by creating a new role using RoleManager and storing the event in the event store.
/// </summary>
/// <param name="roleManager">The role manager instance for role operations.</param>
/// <param name="roleAggregate">The role aggregate instance for creating role creation events.</param>
/// <param name="eventStore">The event store for persisting role creation events.</param>
public class PostRoleHandler(RoleManager<Role> roleManager, RoleAggregate roleAggregate, IEventStore eventStore) : IRequestHandler<PostRoleRequest, PostRoleResponse> 
{
    /// <summary>
    /// Handles a post-role request by checking for existing role, creating a new role, persisting the event, and returning a response.
    /// </summary>
    /// <param name="request">The post-role request containing role creation information.</param>
    /// <param name="cancellationToken">A cancellation token (optional).</param>
    /// <returns>A PostRoleResponse indicating success, conflict, or bad request.</returns>
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