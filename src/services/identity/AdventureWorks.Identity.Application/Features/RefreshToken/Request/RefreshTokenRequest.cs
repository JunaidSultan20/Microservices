using AdventureWorks.Identity.Application.Features.RefreshToken.Response;

namespace AdventureWorks.Identity.Application.Features.RefreshToken.Request;

/// <summary>
/// Represents the request for a refresh token operation.
/// </summary>
/// <remarks>
/// This class implements the IRequest interface and defines the request for a refresh token operation.
/// </remarks>
public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
{
    /// <summary>
    /// Initializes a new instance of the RefreshTokenRequest class.
    /// </summary>
    public RefreshTokenRequest()
    {
    }
}