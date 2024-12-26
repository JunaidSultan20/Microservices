using AdventureWorks.Identity.Application.Features.RefreshToken.Request;
using AdventureWorks.Identity.Application.Features.RefreshToken.Response;

namespace AdventureWorks.Identity.Application.Features.RefreshToken.Handler;

/// <summary>
/// Handles refresh token requests by validating the existing token, retrieving user information, generating a new access token and refresh token, and updating cookies.
/// </summary>
/// <param name="userManager">The user manager instance for user operations.</param>
/// <param name="httpContextAccessor">Provides access to the current HTTP context.</param>
/// <param name="jwtOptions">The options monitor for JWT configuration.</param>
public class RefreshTokenHandler(UserManager<User> userManager,
                                 IHttpContextAccessor httpContextAccessor,
                                 IOptionsMonitor<JwtOptions> jwtOptions) : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.CurrentValue;

    /// <summary>
    /// Handles a refresh token request by retrieving the bearer token from cookies, validating it, finding the user, validating the refresh token, generating a new access token and refresh token, updating cookies, and returning a response.
    /// </summary>
    /// <param name="request">The refresh token request (empty in this case).</param>
    /// <param name="cancellationToken">A cancellation token (optional).</param>
    /// <returns>A RefreshTokenResponse indicating success, unauthorized attempt, not found user, or expired refresh token.</returns>
    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        if (!httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(Constants.BearerToken, out string? bearerToken))
            return new UnauthorizedRefreshTokenResponse(Messages.UnauthorizedAttempt);

        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(token: bearerToken);        
        User? user = await userManager.FindByNameAsync(userName: principal.Identity?.Name ?? string.Empty);
        if (user is null)
            return new NotFoundRefreshTokenResponse(Messages.UserNotFound);

        string? refreshToken = await userManager.GetAuthenticationTokenAsync(user: user, loginProvider: Constants.LoginProviderName, tokenName: Constants.TokenName);
        if (string.IsNullOrEmpty(refreshToken)) 
            throw new RefreshTokenException();

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();        
        if (tokenHandler.CanReadToken(refreshToken))
        {
            JwtSecurityToken token = tokenHandler.ReadJwtToken(refreshToken);
            if (token.ValidTo <= DateTime.UtcNow)
                return new UnauthorizedRefreshTokenResponse(Messages.RefreshTokenExpired);
        }

        JwtSecurityToken refreshedBearerToken = GenerateAccessToken(claims: principal.Claims);
        string newRefreshToken = await userManager.GenerateUserTokenAsync(user: user, tokenProvider: Constants.LoginProviderName, purpose: Constants.TokenName);
        await userManager.SetAuthenticationTokenAsync(user: user, loginProvider: Constants.LoginProviderName, tokenName: Constants.TokenName, tokenValue: newRefreshToken);
        httpContextAccessor.HttpContext?.Response.Cookies.Append(key: Constants.BearerToken, 
                                                                 value: tokenHandler.WriteToken(refreshedBearerToken), 
                                                                 new CookieOptions 
                                                                 {
                                                                     Expires = refreshedBearerToken.ValidTo, 
                                                                     Secure = true, 
                                                                     HttpOnly = true, 
                                                                     IsEssential = true
                                                                 });

        return new RefreshTokenResponse(Messages.BearerTokenRefreshed);
    }

    /// <summary>
    /// Validates an expired JWT token and retrieves the ClaimsPrincipal from it.
    /// </summary>
    /// <param name="token">The expired JWT token string.</param>
    /// <returns>A ClaimsPrincipal object representing the claims from the expired token.</returns>
    /// <exception cref="SecurityTokenException">Thrown if the token is invalid or the signing algorithm is not HmacSha256.</exception>
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(s: _jwtOptions.Secret)),
            ValidateLifetime = false
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal principal = tokenHandler.ValidateToken(token: token,
                                                               validationParameters: tokenValidationParameters,
                                                               validatedToken: out SecurityToken securityToken);
        JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(value: SecurityAlgorithms.HmacSha256,
                                                                            comparisonType: StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException(message: Messages.InvalidAccessToken);

        return principal;
    }

    /// <summary>
    /// Generates a new JWT token with specified claims and expiration time.
    /// </summary>
    /// <param name="claims">The collection of claims to include in the token.</param>
    /// <returns>A new JwtSecurityToken representing the generated access token.</returns>
    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims)
    {
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(s: _jwtOptions.Secret));
        SigningCredentials signinCredentials = new SigningCredentials(key: secretKey, algorithm: SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken token = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
                                                      audience: _jwtOptions.Audience,
                                                      claims: claims,
                                                      expires: DateTime.Now.AddMinutes(_jwtOptions.ExpirationMinutes),
                                                      signingCredentials: signinCredentials);

        return token;
    }
}