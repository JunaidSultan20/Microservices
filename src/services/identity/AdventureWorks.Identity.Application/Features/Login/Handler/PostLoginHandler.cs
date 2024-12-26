using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Application.Features.Login.Handler;

/// <summary>
/// Handles post-login requests by verifying user credentials, generating a JWT token, and setting refresh tokens.
/// </summary>
/// <param name="userManager">The user manager instance for user operations.</param>
/// <param name="httpContextAccessor">Provides access to the current HTTP context.</param>
/// <param name="options">The options monitor for JWT configuration.</param>
public class PostLoginHandler(UserManager<User> userManager, 
                              IHttpContextAccessor httpContextAccessor, 
                              IOptionsMonitor<JwtOptions> options) : IRequestHandler<PostLoginRequest, PostLoginResponse>
{
    private readonly JwtOptions _jwtOptions = options.CurrentValue;

    /// <summary>
    /// Handles a post-login request by attempting to authenticate the user, generate a JWT token, and set refresh tokens.
    /// </summary>
    /// <param name="request">The post-login request containing authentication information.</param>
    /// <param name="cancellationToken">A cancellation token (optional).</param>
    /// <returns>A PostLoginResponse indicating success or unauthorized attempt.</returns>
    public async Task<PostLoginResponse> Handle(PostLoginRequest request, CancellationToken cancellationToken = default)
    {
        HttpContext? context = httpContextAccessor.HttpContext;
        User? user = await userManager.FindByEmailAsync(request.AuthenticationDto?.Email ?? string.Empty);

        if (user is null)
            return new PostUnauthorizedAttemptResponse();

        bool isAuthenticUser = await userManager.CheckPasswordAsync(user: user, 
                                                                    password: request.AuthenticationDto?.Password ?? string.Empty);

        if (!isAuthenticUser)
            return new PostUnauthorizedAttemptResponse();

        IList<string> roles = await userManager.GetRolesAsync(user: user);
        List<Claim> claims = new List<Claim>
        {
            new(type: ClaimTypes.Email, value: user.Email ?? string.Empty),
            new(type: ClaimTypes.Name, value: user.UserName ?? string.Empty),
            new(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
        };
        roles.ToList().ForEach(claim =>
        {
            claims.Add(new Claim(type: ClaimTypes.Role, value: claim));
        });

        SymmetricSecurityKey authenticationSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(_jwtOptions.Secret));
        JwtSecurityToken token = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
                                                      audience: _jwtOptions.Audience,
                                                      expires: DateTime.Now.AddMinutes(value: _jwtOptions.ExpirationMinutes),
                                                      claims: claims,
                                                      signingCredentials: new SigningCredentials(key: authenticationSigningKey,
                                                                                                 algorithm: SecurityAlgorithms.HmacSha256));

        string? refreshToken = await userManager.GetAuthenticationTokenAsync(user: user,
                                                                             loginProvider: Constants.LoginProviderName,
                                                                             tokenName: Constants.TokenName);

        if (!string.IsNullOrEmpty(refreshToken))
            await userManager.RemoveAuthenticationTokenAsync(user: user, loginProvider: Constants.LoginProviderName, tokenName: Constants.TokenName);

        refreshToken = await userManager.GenerateUserTokenAsync(user: user, tokenProvider: Constants.LoginProviderName, purpose: Constants.TokenName);
        await userManager.SetAuthenticationTokenAsync(user: user, loginProvider: Constants.LoginProviderName, tokenName: Constants.TokenName, tokenValue: refreshToken);
        context?.Response.Cookies.Append(key: Constants.BearerToken,
                                         value: new JwtSecurityTokenHandler().WriteToken(token),
                                         new CookieOptions
                                         {
                                             Expires = token.ValidTo,
                                             Secure = true,
                                             HttpOnly = true,
                                             IsEssential = true,
                                             SameSite = SameSiteMode.Strict
                                         });

        return new PostLoginResponse(HttpStatusCode.OK, Messages.BearerTokenGenerated);
    }
}