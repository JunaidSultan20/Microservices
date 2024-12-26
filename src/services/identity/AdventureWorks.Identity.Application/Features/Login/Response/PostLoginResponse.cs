namespace AdventureWorks.Identity.Application.Features.Login.Response;

/// <summary>
/// Represents the response for a post-login operation.
/// </summary>
/// <remarks>
/// This class extends ApiResult and provides a specific response for post-login scenarios.
/// It includes the HTTP status code and an optional message.
/// </remarks>
/// <param name="statusCode">The HTTP status code of the response.</param>
/// <param name="message">An optional message associated with the response.</param>
public class PostLoginResponse(HttpStatusCode statusCode, string? message) : ApiResult(statusCode, message);