namespace Identity.Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User?> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly JwtConfig _jwtConfig;

    public AccountService(UserManager<User?> userManager, RoleManager<Role> roleManager, IOptions<JwtConfig> options)
    {
        _userManager = userManager ?? throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(userManager)));
        _roleManager = roleManager ?? throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(roleManager)));
        _jwtConfig = options.Value ?? throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(options)));
    }

    public async Task<BaseResponse<LoginDto>> LoginAsync(AuthenticationDto authenticationDto)
    {
        User? user = await _userManager.FindByEmailAsync(authenticationDto.Email);
        if (user != null)
        {
            bool isAuthenticUser = await _userManager.CheckPasswordAsync(user, authenticationDto.Password);
            if (isAuthenticUser)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                List<Claim> claims = new List<Claim>
                {
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                roles.ToList().ForEach(x =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, x));
                });
                SymmetricSecurityKey authenticationSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_jwtConfig.Secret ?? string.Empty));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authenticationSigningKey,
                        SecurityAlgorithms.HmacSha256Signature));
                string refreshToken = await _userManager
                    .GetAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    await _userManager.RemoveAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
                    refreshToken = await _userManager
                        .GenerateUserTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
                    await _userManager.SetAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken",
                        refreshToken);
                }
                else
                {
                    refreshToken = await _userManager.GenerateUserTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
                    await _userManager.SetAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken",
                        refreshToken);
                }
                LoginDto loginDto = new LoginDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = DateTime.Now.AddDays(1)
                };
                return new BaseResponse<LoginDto>(HttpStatusCode.OK, "Access Token Generated.", loginDto);
            }
        }

        return new BaseResponse<LoginDto>(HttpStatusCode.Unauthorized, "Unauthorized Attempt");
    }

    public async Task<BaseResponse<UserDto>> RegisterAsync(RegistrationDto registrationDto)
    {
        User? user = await _userManager.FindByEmailAsync(registrationDto.Email);
        if (user != null)
        {
            return new BaseResponse<UserDto>(HttpStatusCode.BadRequest, "User already exists.");
        }

        user = new User
        {
            UserName = registrationDto.UserName,
            NormalizedUserName = registrationDto.UserName?.ToUpper(),
            Email = registrationDto.Email,
            NormalizedEmail = registrationDto.Email?.ToUpper(),
            EmailConfirmed = true
        };
        IdentityResult result = await _userManager.CreateAsync(user, registrationDto.Password);
        if (result.Succeeded)
        {
            if (registrationDto.Roles is not null)
            {
                foreach (string role in registrationDto.Roles)
                {
                    result = await _userManager.AddToRoleAsync(user, role);
                    if (!result.Succeeded)
                        return new BaseResponse<UserDto>(HttpStatusCode.BadRequest, "Unable to create new user")
                            { Errors = result.Errors.Select(x => x.Description).ToList() };
                }
            }

            return new BaseResponse<UserDto>(HttpStatusCode.OK, "User Created Successfully.",
                new UserDto { UserName = user.UserName, Email = user.Email });
        }
        return new BaseResponse<UserDto>(HttpStatusCode.BadRequest, "Unable To Create New User.")
        { Errors = result.Errors.Select(x => x.Description).ToList() };
    }

    public async Task<BaseResponse<RefreshTokenDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(refreshTokenDto.Token);
        string? username = principal.Identity?.Name;
        User? user = await _userManager.FindByNameAsync(username);
        if (user != null)
        {
            string? userToken = await _userManager.GetAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
            if (string.IsNullOrEmpty(userToken))
            {
                return new BaseResponse<RefreshTokenDto>(HttpStatusCode.BadRequest, "Invalid Token Model", refreshTokenDto);
            }
        }
        string newAccessToken = GenerateAccessToken(principal.Claims);
        string newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
        await _userManager.SetAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken", newRefreshToken);
        return new BaseResponse<RefreshTokenDto>(HttpStatusCode.OK, "Token Refreshed Successfully", new RefreshTokenDto
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    public async Task<BaseResponse<object>> LogoutAsync(string token)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
        User? user = await _userManager.FindByIdAsync(jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        IdentityResult result =
            await _userManager.RemoveAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
        if (!result.Succeeded)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, "Unable To Logout User.", null)
            { Errors = result.Errors.Select(x => x.Description).ToList() };
        return new BaseResponse<object>(HttpStatusCode.OK, "User Logged Out Successfully.", null);
    }

    public async Task<BaseResponse<object>> ChangePassword(string email, string currentPassword, string newPassword)
    {
        User? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, "Invalid username.", null);
        IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, "Unable to change password.", null);
        return new BaseResponse<object>(HttpStatusCode.OK, $"Password updated successfully for {email}.", null);
    }

    public async Task AddRole(string roleName)
    {
        await _roleManager.CreateAsync(new Role { Name = roleName, NormalizedName = roleName.ToUpper() });
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_jwtConfig.Secret ?? string.Empty)),
            ValidateLifetime = false
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid access token provided.");
        return principal;
    }

    private string GenerateAccessToken(IEnumerable<Claim>? claims)
    {
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_jwtConfig.Secret ?? string.Empty));
        SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken tokeOptions = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: "http://localhost:4100",
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }
}