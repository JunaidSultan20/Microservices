namespace AdventureWorks.Identity.Application.Features.RefreshToken.Handler;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenHandler(UserManager<User> userManager,
                                  IHttpContextAccessor httpContextAccessor,
                                  IOptions<JwtOptions> options)
    {
        _userManager = userManager ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage,
            innerException: new ArgumentNullException(nameof(userManager)));
        _httpContextAccessor = httpContextAccessor ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage,
            innerException: new ArgumentNullException(nameof(httpContextAccessor)));
        _jwtOptions = options.Value ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage,
            innerException: new ArgumentNullException(nameof(options)));
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        if (!_httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(Constants.BearerToken, out string? bearerToken))
            return new UnauthorizedRefreshTokenResponse(Messages.UnauthorizedAttempt);

        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(token: bearerToken);

        string? username = principal.Identity?.Name;

        User? user = await _userManager.FindByNameAsync(userName: username ?? string.Empty);

        if (user is null)
            return new NotFoundRefreshTokenResponse(Messages.UserNotFound);

        string? refreshToken = await _userManager.GetAuthenticationTokenAsync(user: user,
                                                                              loginProvider: Constants.LoginProviderName,
                                                                              tokenName: Constants.TokenName);
        if (string.IsNullOrEmpty(refreshToken))
            throw new RefreshTokenException();

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken token;
        if (tokenHandler.CanReadToken(refreshToken))
        {
            token = tokenHandler.ReadJwtToken(refreshToken);
            var tokenExpiration = token.ValidTo;

            if (tokenExpiration <= DateTime.UtcNow)
                return new UnauthorizedRefreshTokenResponse(Messages.RefreshTokenExpired);
        }

        JwtSecurityToken refreshedBearerToken = GenerateAccessToken(claims: principal.Claims);

        string newRefreshToken = await _userManager.GenerateUserTokenAsync(user: user,
                                                                           tokenProvider: Constants.LoginProviderName,
                                                                           purpose: Constants.TokenName);

        await _userManager.SetAuthenticationTokenAsync(user: user,
                                                       loginProvider: Constants.LoginProviderName,
                                                       tokenName: Constants.TokenName,
                                                       tokenValue: newRefreshToken);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key: Constants.BearerToken,
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

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(s: _jwtOptions.Secret ?? string.Empty)),
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

    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims)
    {
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(s: _jwtOptions.Secret ?? string.Empty));

        SigningCredentials signinCredentials = new SigningCredentials(key: secretKey, algorithm: SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
                                                      audience: "http://localhost:4100",
                                                      claims: claims,
                                                      expires: DateTime.Now.AddMinutes(_jwtOptions.ExpirationMinutes),
                                                      signingCredentials: signinCredentials);
        return token;
    }
}