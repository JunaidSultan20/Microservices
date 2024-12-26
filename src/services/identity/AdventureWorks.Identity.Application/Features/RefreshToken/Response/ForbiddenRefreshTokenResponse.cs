namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

/// <summary>
/// Represents the response for a forbidden refresh token operation.
/// </summary>
/// <remarks>
/// This class extends RefreshTokenResponse and provides a specific response for forbidden scenarios.
/// It sets the HTTP status code to Forbidden and includes a custom message indicating the authentication cookie is missing.
/// </remarks>
public class ForbiddenRefreshTokenResponse() : RefreshTokenResponse(HttpStatusCode.Forbidden, "Authentication cookie missing");