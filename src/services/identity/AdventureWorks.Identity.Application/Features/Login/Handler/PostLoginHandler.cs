using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Application.Features.Login.Handler;

public class PostLoginHandler(UserManager<User> userManager, 
                              IHttpContextAccessor httpContextAccessor, 
                              JwtOptions jwtOptions) : IRequestHandler<PostLoginRequest, PostLoginResponse>
{
    public async Task<PostLoginResponse> Handle(PostLoginRequest request, CancellationToken cancellationToken = default)
    {
        var context = httpContextAccessor.HttpContext;

        User? user = await userManager.FindByEmailAsync(request.AuthenticationDto?.Email ?? string.Empty);

        if (user is not null)
        {
            bool isAuthenticUser = await userManager.CheckPasswordAsync(user: user, 
                                                                        password: request.AuthenticationDto?.Password ?? string.Empty);
            if (isAuthenticUser)
            {
                IList<string> roles = await userManager.GetRolesAsync(user: user);

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

                SymmetricSecurityKey authenticationSigningKey = new SymmetricSecurityKey(key: Encoding.UTF32.GetBytes(jwtOptions.Secret));

                JwtSecurityToken token = new JwtSecurityToken(issuer: jwtOptions.Issuer,
                                                              audience: jwtOptions.Audience,
                                                              expires: DateTime.Now.AddMinutes(value: jwtOptions.ExpirationMinutes),
                                                              claims: claims,
                                                              signingCredentials: new SigningCredentials(key: authenticationSigningKey,
                                                                                                         algorithm: SecurityAlgorithms.HmacSha256));

                string? refreshToken = await userManager.GetAuthenticationTokenAsync(user: user, 
                                                                                     loginProvider: Constants.LoginProviderName, 
                                                                                     tokenName: Constants.TokenName);

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    await userManager.RemoveAuthenticationTokenAsync(user: user, 
                                                                     loginProvider: Constants.LoginProviderName, 
                                                                     tokenName: Constants.TokenName);
                }

                refreshToken = await userManager.GenerateUserTokenAsync(user: user, 
                                                                        tokenProvider: Constants.LoginProviderName, 
                                                                        purpose: Constants.TokenName);

                await userManager.SetAuthenticationTokenAsync(user: user, 
                                                              loginProvider: Constants.LoginProviderName, 
                                                              tokenName: Constants.TokenName, 
                                                              tokenValue: refreshToken);

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
        return new PostUnauthorizedAttemptResponse();
    }
}