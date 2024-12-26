namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

/// <summary>
/// Represents the response for a post-role operation.
/// </summary>
/// <remarks>
/// This class extends ApiResult and provides a specific response for post-role scenarios.
/// It includes the HTTP status code and a message indicating the result of the role creation.
/// </remarks>
/// <param name="statusCode">The HTTP status code of the response.</param>
/// <param name="message">A message indicating the result of the role creation.</param>
public class PostRoleResponse(HttpStatusCode statusCode, string message) : ApiResult(statusCode, message);