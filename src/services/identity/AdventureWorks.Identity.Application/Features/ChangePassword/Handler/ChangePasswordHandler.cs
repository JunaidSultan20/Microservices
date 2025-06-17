using AdventureWorks.Identity.Application.Features.ChangePassword.Request;
using AdventureWorks.Identity.Application.Features.ChangePassword.Response;

namespace AdventureWorks.Identity.Application.Features.ChangePassword.Handler;

public class ChangePasswordHandler(UserManager<User> userManager) : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
{
    public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request,
                                                     CancellationToken cancellationToken = default)
    {
        User? user = await userManager.FindByEmailAsync(request.ChangePasswordDto?.Email ?? string.Empty);

        if (user is null)
            return new ChangePasswordUnauthorizedResponse();

        IdentityResult result = await userManager.ChangePasswordAsync(user, 
                                                                      request.ChangePasswordDto?.CurrentPassword ?? string.Empty, 
                                                                      request.ChangePasswordDto?.NewPassword ?? string.Empty);

        if (!result.Succeeded)
            return new BadRequestChangePasswordResponse();

        return new ChangePasswordResponse(HttpStatusCode.NoContent, $"{Messages.PasswordUpdatedFor} {request.ChangePasswordDto?.Email}");
    }
}