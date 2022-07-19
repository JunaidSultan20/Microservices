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

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, BaseResponse<RefreshTokenDto>>
{
    private readonly UserManager<User?> _userManager;
    private readonly JwtConfig _jwtConfig;

    public RefreshTokenQueryHandler(UserManager<User?> userManager, IOptions<JwtConfig> options)
    {
        _userManager = userManager ??
                       throw new Exception("Argument null exception", new ArgumentNullException(nameof(userManager)));
        _jwtConfig = options.Value ??
                     throw new Exception("Argument null exception", new ArgumentNullException(nameof(options)));
    }

    public async Task<BaseResponse<RefreshTokenDto>> Handle(RefreshTokenQuery request,
        CancellationToken cancellationToken = default)
    {
        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(request.RefreshTokenDto?.Token);
        string? username = principal.Identity?.Name;
        User? user = await _userManager.FindByNameAsync(username);
        if (user != null)
        {
            string? userToken = await _userManager.GetAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
            if (string.IsNullOrEmpty(userToken))
            {
                return new BaseResponse<RefreshTokenDto>(HttpStatusCode.BadRequest, "Invalid Token Model", request.RefreshTokenDto);
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