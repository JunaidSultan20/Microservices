namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

/// <summary>
/// Represents the response for a bad request during a post-role operation.
/// </summary>
/// <remarks>
/// This class extends PostRoleResponse and provides a specific response for bad requests.
/// It sets the HTTP status code to BadRequest and includes a custom message indicating the error.
/// </remarks>
/// <param name="message">A custom message indicating the reason for the bad request.</param>
public class BadRequestPostRoleResponse(string message) : PostRoleResponse(HttpStatusCode.BadRequest, message);