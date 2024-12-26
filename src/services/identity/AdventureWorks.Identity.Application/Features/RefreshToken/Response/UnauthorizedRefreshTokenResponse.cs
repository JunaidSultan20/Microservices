namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

/// <summary>
/// Represents the response for an unauthorized refresh token attempt.
/// </summary>
/// <remarks>
/// This class extends RefreshTokenResponse and provides a specific response for unauthorized attempts.
/// It sets the HTTP status code to Unauthorized and includes a custom message indicating the error.
/// </remarks>
/// <param name="message">A custom message indicating the reason for the unauthorized attempt.</param>
public class UnauthorizedRefreshTokenResponse(string message) : RefreshTokenResponse(HttpStatusCode.Unauthorized, message);