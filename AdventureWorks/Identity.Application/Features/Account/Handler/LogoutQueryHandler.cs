using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AdventureWorks.Common.Response;
using Identity.Application.Features.Account.Query;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Account.Handler;

public class LogoutQueryHandler : IRequestHandler<LogoutQuery, BaseResponse<object>>
{
    private readonly UserManager<User?> _userManager;

    public LogoutQueryHandler(UserManager<User?> userManager)
    {
        _userManager = userManager ??
                       throw new Exception("Argument null exception", new ArgumentNullException(nameof(userManager)));
    }

    public async Task<BaseResponse<object>> Handle(LogoutQuery request, CancellationToken cancellationToken = default)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
        User? user = await _userManager.FindByIdAsync(jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        IdentityResult result =
            await _userManager.RemoveAuthenticationTokenAsync(user, "AdventureWorks.Identity", "RefreshToken");
        if (!result.Succeeded)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, "Unable To Logout User.", null)
                { Errors = result.Errors.Select(x => x.Description).ToList() };
        return new BaseResponse<object>(HttpStatusCode.OK, "User Logged Out Successfully.", null);
    }
}