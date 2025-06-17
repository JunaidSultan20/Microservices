using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AdventureWorks.Common.Helpers;

/// <summary>
/// Provides helper methods for working with JWT tokens.
/// </summary>
public static class TokenHelper
{
    /// <summary>
    /// Extracts the email address from the specified JWT token.
    /// </summary>
    /// <param name="token">The JWT token as a string.</param>
    /// <returns>The email address extracted from the token, or an empty string if not found.</returns>
    /// <exception cref="ArgumentException">Thrown if the provided token is null or invalid.</exception>
    public static string GetUserEmail(string token)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken? jwtToken = handler.ReadJwtToken(token);
        Claim? emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
        return emailClaim?.Value ?? string.Empty;
    }
}