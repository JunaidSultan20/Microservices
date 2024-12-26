using AdventureWorks.Identity.Application.Features.PostRole.Request;
using AdventureWorks.Identity.Application.Features.PostRole.Response;

namespace AdventureWorks.Identity.Api.Controllers;

/// <summary>
/// Role controller that contains the endpoints to manage the roles related activities.
/// </summary>
/// <param name="serviceProvider">
/// An instance of IServiceProvider that is used to resolve services.
/// </param>
[Produces(contentType: Constants.ContentTypeJson)]
public class RoleController(IServiceProvider serviceProvider) : BaseController<RoleController>(serviceProvider)
{
    /// <summary>
    /// Adds a new role to the system and returns the appropriate response based on the result.
    /// </summary>
    /// <remarks>
    /// This endpoint handles the addition of a new role by accepting a role creation DTO, processing the request, and 
    /// returning a response indicating whether the role creation was successful or encountered conflicts or errors.
    /// </remarks>
    /// <param name="createRoleDto">The data transfer object containing the new role's details.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    ///   <para>Returns the <see cref="PostRoleResponse"/> indicating the result of the role creation operation.</para>
    ///   <para>If successful, returns HTTP status code 201 (Created) along with the newly created role.</para>
    ///   <para>If a role with the same name already exists, returns HTTP status code 409 (Conflict).</para>
    ///   <para>For other issues, returns HTTP status code 400 (Bad Request).</para>
    /// </returns>
    /// <response code="201">Returns the <see cref="PostRoleResponse"/> if the role is successfully created.</response>
    /// <response code="409">Returns a <see cref="ConflictPostRoleResponse"/> if a role with the same name already exists.</response>
    /// <response code="400">Returns a <see cref="BadRequestPostRoleResponse"/> if the request is invalid or an error occurred during the process.</response>
    /// <example>
    ///     POST /api/roles
    ///     Request:
    ///     {
    ///         "roleName": "Admin"
    ///     }
    ///     Response:
    ///     HTTP/1.1 201 Created
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 201,
    ///         "message": "Role created successfully",
    ///         "role": {
    ///             "id": "some-unique-id",
    ///             "roleName": "Admin"
    ///         }
    ///     }
    /// 
    ///     OR
    /// 
    ///     HTTP/1.1 409 Conflict
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 409,
    ///         "message": "Role already exists"
    ///     }
    /// 
    ///     OR
    /// 
    ///     HTTP/1.1 400 Bad Request
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 400,
    ///         "message": "Invalid role creation request"
    ///     }
    /// </example>
    [HttpPost(Name = "AddRole")]
    [SwaggerRequestExample(typeof(PostRoleRequest), typeof(PostRoleRequestExample))]
    [ProducesResponseType(typeof(BadRequestPostRoleResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ConflictPostRoleResponse), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(PostRoleResponse), (int)HttpStatusCode.Created)]
    [RequiresParameter(Name = nameof(CreateRoleDto), Required = true, Source = OpenApiParameterLocation.Body, Type = typeof(CreateRoleDto))]
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