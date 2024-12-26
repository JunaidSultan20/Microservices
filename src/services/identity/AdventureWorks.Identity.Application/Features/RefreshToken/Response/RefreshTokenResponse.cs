namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

/// <summary>
/// Represents the response for a refresh token operation.
/// </summary>
/// <remarks>
/// This class extends ApiResult and provides a specific response for refresh token scenarios.
/// It includes the HTTP status code and a message indicating the result of the refresh token operation.
/// </remarks>
/// <param name="statusCode">The HTTP status code of the response.</param>
/// <param name="message">A message indicating the result of the refresh token operation.</param>
public class RefreshTokenResponse(HttpStatusCode statusCode, string message) : ApiResult(statusCode, message)
{
    /// <summary>
    /// Initializes a new instance of the RefreshTokenResponse class with a specified message and a default HTTP status code of OK.
    /// </summary>
    /// <param name="message">A message indicating the result of the refresh token operation.</param>
    public RefreshTokenResponse(string message) : this(HttpStatusCode.OK, message)
    {
    }
}