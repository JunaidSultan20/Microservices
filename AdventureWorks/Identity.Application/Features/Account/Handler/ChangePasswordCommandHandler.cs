using System.Net;
using AdventureWorks.Common.Response;
using Identity.Application.Features.Account.Command;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Account.Handler;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, BaseResponse<object>>
{
    private readonly UserManager<User?> _userManager;

    public ChangePasswordCommandHandler(UserManager<User?> userManager)
    {
        _userManager = userManager ??
                       throw new Exception("Argument null exception", new ArgumentNullException(nameof(userManager)));
    }

    public async Task<BaseResponse<object>> Handle(ChangePasswordCommand request,
        CancellationToken cancellationToken = default)
    {
        User? user = await _userManager.FindByEmailAsync(request.ChangePasswordDto?.Email);
        if (user is null)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, "Invalid username.", null);
        IdentityResult result = await _userManager.ChangePasswordAsync(user, request.ChangePasswordDto?.CurrentPassword, request.ChangePasswordDto?.NewPassword);
        if (!result.Succeeded)
            return new BaseResponse<object>(HttpStatusCode.BadRequest, "Unable to change password.", null);
        return new BaseResponse<object>(HttpStatusCode.OK, $"Password updated successfully for {request.ChangePasswordDto?.Email}.", null);
    }
}