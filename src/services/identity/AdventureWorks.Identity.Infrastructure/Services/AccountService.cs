//namespace Identity.Infrastructure.Services;

//public class AccountService : IAccountService
//{
//	private readonly UserManager<User> _userManager;
//	private readonly RoleManager<Role> _roleManager;
//	private readonly JwtOptions _jwtConfig;

//	public AccountService(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JwtOptions> options)
//	{
//		_userManager = userManager ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage, 
//                                                          innerException: new ArgumentNullException(nameof(userManager)));

//        _roleManager = roleManager ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage, 
//                                                          innerException: new ArgumentNullException(nameof(roleManager)));

//        _jwtConfig = options.Value ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage, 
//                                                          innerException: new ArgumentNullException(nameof(options)));
//	}

//	public async Task<BaseResponse<LoginDto>> LoginAsync(AuthenticationDto authenticationDto)
//	{
//		User? user = await _userManager.FindByEmailAsync(email: authenticationDto.Email ?? string.Empty);

//        if (user is not null)
//		{
//			bool isAuthenticUser = await _userManager.CheckPasswordAsync(user: user, password: authenticationDto.Password ?? string.Empty);

//            if (isAuthenticUser)
//			{
//				IList<string> roles = await _userManager.GetRolesAsync(user: user);

//                List<Claim> claims = new List<Claim>
//				{
//					new(type: ClaimTypes.Email, value: user.Email ?? string.Empty),
//					new(type: ClaimTypes.Name, value: user.UserName ?? string.Empty),
//					new(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
//					new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
//				};

//                roles.ToList().ForEach(item =>
//				{
//					claims.Add(new Claim(type: ClaimTypes.Role, value: item));
//				});

//                SymmetricSecurityKey authenticationSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32
//                                                                                         .GetBytes(s: _jwtConfig.Secret ?? string.Empty));

//                JwtSecurityToken token = new JwtSecurityToken(issuer: _jwtConfig.Issuer, 
//                                                              audience: _jwtConfig.Audience,
//                                                              expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes), 
//                                                              claims: claims, 
//                                                              signingCredentials: new SigningCredentials(key: authenticationSigningKey, 
//                                                                                                         algorithm: SecurityAlgorithms.HmacSha256Signature));

//                string? refreshToken = await _userManager.GetAuthenticationTokenAsync(user: user, 
//                                                                                      loginProvider: Constants.LoginProviderName, 
//                                                                                      tokenName: Constants.TokenName);

//                if (!string.IsNullOrEmpty(refreshToken))
//				{
//					await _userManager.RemoveAuthenticationTokenAsync(user: user,
//                                                                      loginProvider: Constants.LoginProviderName,
//                                                                      tokenName: Constants.TokenName);

//                    refreshToken = await _userManager.GenerateUserTokenAsync(user: user, 
//                                                                             tokenProvider: Constants.LoginProviderName, 
//                                                                             purpose: Constants.TokenName);

//                    await _userManager.SetAuthenticationTokenAsync(user: user, 
//                                                                   loginProvider: Constants.LoginProviderName, 
//                                                                   tokenName: Constants.TokenName, 
//                                                                   tokenValue: refreshToken);
//				}
//				else
//				{
//					refreshToken = await _userManager.GenerateUserTokenAsync(user: user, 
//                                                                             tokenProvider: Constants.LoginProviderName, 
//                                                                             purpose: Constants.TokenName);

//                    await _userManager.SetAuthenticationTokenAsync(user: user, 
//                                                                   loginProvider: Constants.LoginProviderName, 
//                                                                   tokenName: Constants.TokenName, 
//                                                                   tokenValue: refreshToken);
//				}

//                LoginDto loginDto = new LoginDto(token: new JwtSecurityTokenHandler().WriteToken(token: token),
//                                                 expiration: token.ValidTo,
//                                                 refreshToken: refreshToken,
//                                                 refreshTokenExpiration: DateTime.Now.AddDays(1));

//                return new BaseResponse<LoginDto>(statusCode: HttpStatusCode.OK, message: Messages.BearerTokenGenerated, result: loginDto);
//			}
//		}

//		return new BaseResponse<LoginDto>(statusCode: HttpStatusCode.Unauthorized, message: Messages.UnauthorizedAttempt);
//	}

//	public async Task<BaseResponse<UserDto>> RegisterAsync(RegistrationDto registrationDto)
//	{
//		User? user = await _userManager.FindByEmailAsync(email: registrationDto.Email ?? string.Empty);

//        if (user != null)
//            return new BaseResponse<UserDto>(statusCode: HttpStatusCode.BadRequest, message: Messages.UserExists);

//        user = new User(username: registrationDto.UserName, 
//                        normalizedUsername: registrationDto.UserName?.ToUpper(), 
//                        email: registrationDto.Email, 
//                        normalizedEmail: registrationDto.Email?.ToUpper(), 
//                        emailConfirmed: true);

//		IdentityResult result = await _userManager.CreateAsync(user: user, password: registrationDto.Password ?? string.Empty);

//		if (!result.Succeeded)
//            return new BaseResponse<UserDto>(statusCode: HttpStatusCode.BadRequest, 
//                                             message: Messages.UnableToCreateUser, 
//                                             errors: result.Errors.Select(x => x.Description).ToList());

//        if (registrationDto.Roles is not null)
//        {
//            foreach (string role in registrationDto.Roles)
//            {
//                result = await _userManager.AddToRoleAsync(user, role);

//                if (!result.Succeeded)
//                    return new BaseResponse<UserDto>(statusCode: HttpStatusCode.BadRequest,
//                                                     message: Messages.UnableToCreateUser,
//                                                     errors: result.Errors.Select(x => x.Description).ToList());
//            }
//        }

//        return new BaseResponse<UserDto>(statusCode: HttpStatusCode.OK, 
//                                         message: Messages.UserCreatedSuccessfully,
//                                         result: new UserDto(userName: user.UserName, 
//                                                             email: user.Email));
//    }

//	public async Task<BaseResponse<RefreshTokenDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
//	{
//		ClaimsPrincipal principal = GetPrincipalFromExpiredToken(token: refreshTokenDto.Token);

//        string? userName = principal.Identity?.Name;

//        User? user = await _userManager.FindByNameAsync(userName: userName ?? string.Empty);

//        if (user != null)
//		{
//			string? userToken = await _userManager.GetAuthenticationTokenAsync(user: user, 
//                                                                               loginProvider: Constants.LoginProviderName, 
//                                                                               tokenName: Constants.TokenName);

//            if (string.IsNullOrEmpty(userToken))
//                return new BaseResponse<RefreshTokenDto>(statusCode: HttpStatusCode.BadRequest, 
//                                                         message: Messages.InvalidAccessToken, 
//														 result: refreshTokenDto);
//        }

//		string newAccessToken = GenerateAccessToken(claims: principal.Claims);

//		string newRefreshToken = await _userManager.GenerateUserTokenAsync(user: user, 
//                                                                           tokenProvider: Constants.LoginProviderName,
//                                                                           purpose: Constants.TokenName);

//        await _userManager.SetAuthenticationTokenAsync(user: user,
//                                                       loginProvider: Constants.LoginProviderName, 
//                                                       tokenName: Constants.TokenName, 
//                                                       tokenValue: newRefreshToken);

//        return new BaseResponse<RefreshTokenDto>(statusCode: HttpStatusCode.OK,
//                                                 message: Messages.BearerTokenRefreshed,
//                                                 result: new RefreshTokenDto(token: newAccessToken,
//                                                                             refreshToken: newRefreshToken));
//	}

//	public async Task<BaseResponse<object>> LogoutAsync(string token)
//	{
//		JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

//		JwtSecurityToken jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token: token);

//		User? user = await _userManager.FindByIdAsync(userId: jwtToken.Claims
//                                                                      .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
//                                                                      .Value ?? string.Empty);

//        IdentityResult result = await _userManager.RemoveAuthenticationTokenAsync(user: user, 
//                                                                                  loginProvider: Constants.LoginProviderName, 
//                                                                                  tokenName: Constants.TokenName);

//        if (!result.Succeeded)
//			return new BaseResponse<object>(statusCode: HttpStatusCode.BadRequest, 
//                                            message: Messages.LogoutFailed, 
//                                            errors: result.Errors.Select(x => x.Description).ToList());

//		return new BaseResponse<object>(statusCode: HttpStatusCode.OK, 
//                                        message: Messages.LogoutSuccessful);
//	}

//	public async Task<BaseResponse<object>> ChangePassword(string email, string currentPassword, string newPassword)
//	{
//		User? user = await _userManager.FindByEmailAsync(email: email);

//		if (user is null)
//			return new BaseResponse<object>(statusCode: HttpStatusCode.BadRequest, 
//                                            message: Messages.InvalidUsername);

//		IdentityResult result = await _userManager.ChangePasswordAsync(user: user, 
//                                                                       currentPassword: currentPassword, 
//                                                                       newPassword: newPassword);

//        if (!result.Succeeded)
//			return new BaseResponse<object>(statusCode: HttpStatusCode.BadRequest, 
//                                            message: Messages.UnableToChangePassword);

//		return new BaseResponse<object>(statusCode: HttpStatusCode.OK, 
//                                        message: $"{Messages.PasswordUpdatedFor} {email}");
//	}

//	public async Task AddRole(string roleName)
//	{
//		await _roleManager.CreateAsync(role: new Role(name: roleName, 
//                                                      normalizedName: roleName.ToUpper()));
//	}

//	private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
//	{
//		TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
//		{
//			ValidateAudience = false,
//			ValidateIssuer = false,
//			ValidateIssuerSigningKey = true,
//			IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(s: _jwtConfig.Secret ?? string.Empty)),
//			ValidateLifetime = false
//		};

//		JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

//		ClaimsPrincipal principal = tokenHandler.ValidateToken(token: token, 
//                                                               validationParameters: tokenValidationParameters,
//                                                               validatedToken: out SecurityToken securityToken);

//		JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

//		if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(value: SecurityAlgorithms.HmacSha256Signature, 
//                                                                            comparisonType: StringComparison.InvariantCultureIgnoreCase))
//            throw new SecurityTokenException(message: Messages.InvalidAccessToken);

//		return principal;
//	}

//	private string GenerateAccessToken(IEnumerable<Claim>? claims)
//	{
//		SymmetricSecurityKey secretKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(s: _jwtConfig.Secret ?? string.Empty));

//		SigningCredentials signinCredentials = new SigningCredentials(key: secretKey, algorithm: SecurityAlgorithms.HmacSha256Signature);

//		JwtSecurityToken tokenOptions = new JwtSecurityToken(issuer: _jwtConfig.Issuer, 
//                                                             audience: "http://localhost:4100", 
//                                                             claims: claims, 
//                                                             expires: DateTime.Now.AddMinutes(value: 5), 
//                                                             signingCredentials: signinCredentials);

//		string tokenString = new JwtSecurityTokenHandler().WriteToken(token: tokenOptions);

//		return tokenString;
//	}
//}