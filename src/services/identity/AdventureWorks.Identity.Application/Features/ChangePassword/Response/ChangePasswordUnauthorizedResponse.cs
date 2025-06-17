namespace AdventureWorks.Identity.Application.Features.ChangePassword.Response;

/// <summary>
/// Represents the response for an unauthorized login attempt.
/// </summary>
/// <remarks>
/// This class extends ChangePasswordResponse and provides a specific response for unauthorized attempts.
/// It sets the HTTP status code to Unauthorized and the message to indicate an unauthorized attempt.
/// </remarks>
public class ChangePasswordUnauthorizedResponse() : ChangePasswordResponse(HttpStatusCode.Unauthorized, Messages.UnauthorizedAttempt);