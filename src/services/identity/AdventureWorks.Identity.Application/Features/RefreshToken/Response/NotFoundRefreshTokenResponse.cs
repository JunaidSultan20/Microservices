namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

/// <summary>
/// Represents the response for a not found refresh token operation.
/// </summary>
/// <remarks>
/// This class extends RefreshTokenResponse and provides a specific response for not found scenarios.
/// It sets the HTTP status code to NotFound and includes a custom message indicating the user was not found.
/// </remarks>
/// <param name="message">A custom message indicating the reason for the not found error.</param>
public class NotFoundRefreshTokenResponse(string message) : RefreshTokenResponse(HttpStatusCode.NotFound, Messages.UserNotFound);