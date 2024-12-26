namespace AdventureWorks.Identity.Application.Features.Login.Response;

/// <summary>
/// Represents the response for an unauthorized login attempt.
/// </summary>
/// <remarks>
/// This class extends PostLoginResponse and provides a specific response for unauthorized attempts.
/// It sets the HTTP status code to Unauthorized and the message to indicate an unauthorized attempt.
/// </remarks>
public class PostUnauthorizedAttemptResponse() : PostLoginResponse(HttpStatusCode.Unauthorized, Messages.UnauthorizedAttempt);