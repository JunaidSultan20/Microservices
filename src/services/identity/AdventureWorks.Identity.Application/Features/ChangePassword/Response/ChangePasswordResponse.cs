namespace AdventureWorks.Identity.Application.Features.ChangePassword.Response;

/// <summary>
/// Represents the response for change password operation.
/// </summary>
/// <remarks>
/// This class extends ApiResult and provides a specific response for change password scenarios.
/// It includes the HTTP status code and an optional message.
/// </remarks>
/// <param name="statusCode">The HTTP status code of the response.</param>
/// <param name="message">An optional message associated with the response.</param>
public class ChangePasswordResponse(HttpStatusCode statusCode, string? message) : ApiResult(statusCode, message);