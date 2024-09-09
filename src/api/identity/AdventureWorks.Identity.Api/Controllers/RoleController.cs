using AdventureWorks.Identity.Application.Features.PostRole.Request;
using AdventureWorks.Identity.Application.Features.PostRole.Response;

namespace AdventureWorks.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RoleController(IServiceProvider serviceProvider) : BaseController<RoleController>(serviceProvider)
{
    /// <summary>
    /// Adds a new role to the system.
    /// </summary>
    /// <param name="createRoleDto">The name of the role to add.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
    /// </param>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing a <see cref="PostRoleResponse"/>.
    /// Returns <see cref="HttpStatusCode.Created"/> if the role is successfully created,
    /// <see cref="HttpStatusCode.Conflict"/> if the role already exists, 
    /// or <see cref="HttpStatusCode.BadRequest"/> if the request is invalid.
    /// </returns>
    /// <response code="201">The role was successfully created.</response>
    /// <response code="409">A role with the same name already exists.</response>
    /// <response code="400">The request is invalid.</response>
    /// <exception cref="ArgumentNullException">Thrown when the role name is null.</exception>
    [HttpPost(Name = "AddRole")]
    [SwaggerRequestExample(typeof(PostRoleRequest), typeof(PostRoleRequestExample))]
    [ProducesResponseType(typeof(BadRequestPostRoleResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ConflictPostRoleResponse), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(PostRoleResponse), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<PostRoleResponse>> AddRole([FromBody] CreateRoleDto createRoleDto,
                                                              CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(argument: createRoleDto, paramName: nameof(createRoleDto));
        PostRoleResponse response = await Mediator.Send(new PostRoleRequest(createRoleDto), cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => BadRequest(response)
        };
    }
}