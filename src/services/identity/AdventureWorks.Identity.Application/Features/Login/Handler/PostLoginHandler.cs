namespace AdventureWorks.Identity.Application.Features.Login.Handler;

public class PostLoginHandler : IRequestHandler<PostLoginRequest, PostLoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtOptions _jwtOptions;

    public PostLoginHandler(UserManager<User> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<PostLoginResponse> Handle(PostLoginRequest request, CancellationToken cancellationToken = default)
    {
        var context = _httpContextAccessor.HttpContext;

        User? user = await _userManager.FindByEmailAsync(request.AuthenticationDto?.Email ?? string.Empty);

        if (user is not null)
        {
            bool isAuthenticUser = await _userManager.CheckPasswordAsync(user: user,
                                                                         password: request.AuthenticationDto?.Password
                                                                         ?? string.Empty);
            if (isAuthenticUser)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user: user);

                List<Claim> claims = new List<Claim>
                {
                    new(type: ClaimTypes.Email, value: user.Email ?? string.Empty),
                    new(type: ClaimTypes.Name, value: user.UserName ?? string.Empty),
                    new(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
                    new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
                };

                roles.ToList().ForEach(action: claim =>
                {
                    claims.Add(new Claim(type: ClaimTypes.Role, value: claim));
                });

                SymmetricSecurityKey authenticationSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32
                                                                    .GetBytes(s: _jwtOptions.Secret ?? string.Empty));

                JwtSecurityToken token = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
                                                              audience: _jwtOptions.Audience,
                                                              expires: DateTime.Now.AddMinutes(value: _jwtOptions.ExpirationMinutes),
                                                              claims: claims,
                                                              signingCredentials: new SigningCredentials(key: authenticationSigningKey,
                                                                                                         algorithm: SecurityAlgorithms.HmacSha256));

                string? refreshToken = await _userManager.GetAuthenticationTokenAsync(user: user,
                                                                                      loginProvider: Constants.LoginProviderName,
                                                                                      tokenName: Constants.TokenName);

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    await _userManager.RemoveAuthenticationTokenAsync(user: user,
                                                                      loginProvider: Constants.LoginProviderName,
                                                                      tokenName: Constants.TokenName);
                }

                refreshToken = await _userManager.GenerateUserTokenAsync(user: user,
                                                                         tokenProvider: Constants.LoginProviderName,
                                                                         purpose: Constants.TokenName);

                await _userManager.SetAuthenticationTokenAsync(user: user,
                                                               loginProvider: Constants.LoginProviderName,
                                                               tokenName: Constants.TokenName,
                                                               tokenValue: refreshToken);

                context.Response.Cookies.Append(key: Constants.BearerToken,
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

        return new PostUnauthorizedAttemptResponse();
    }
}
