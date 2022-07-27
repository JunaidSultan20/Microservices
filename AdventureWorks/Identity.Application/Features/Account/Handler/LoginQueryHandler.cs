using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AdventureWorks.Common.Response;
using Identity.Application.DTOs;
using Identity.Application.Features.Account.Query;
using Identity.Domain.Entities;
using Identity.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Application.Features.Account.Handler;

public class LoginQueryHandler : IRequestHandler<LoginQuery, BaseResponse<LoginDto>>
{
    private readonly UserManager<User?> _userManager;
    private readonly JwtConfig _jwtConfig;

    public LoginQueryHandler(UserManager<User?> userManager, RoleManager<Role?> roleManager, IOptions<JwtConfig> options)
    {
        _userManager = userManager ??
                       throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(userManager)));
        _jwtConfig = options.Value;
    }

    public async Task<BaseResponse<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken = default)
    {
        User? user = await _userManager.FindByEmailAsync(request.AuthenticationDto?.Email);
        if (user != null)
        {
            bool isAuthenticUser = await _userManager.CheckPasswordAsync(user, request.AuthenticationDto?.Password);
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
}