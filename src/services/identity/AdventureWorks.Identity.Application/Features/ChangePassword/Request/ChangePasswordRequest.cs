using AdventureWorks.Identity.Application.Features.ChangePassword.Response;

namespace AdventureWorks.Identity.Application.Features.ChangePassword.Request;

/// <summary>
/// Represents the request for a change password operation.
/// </summary>
/// <remarks>
/// This class implements the IRequest interface and defines the request for a change password operation.
/// It includes a ChangePasswordDto property to hold the password change information.
/// </remarks>
/// <param name="changePasswordDto">The password information for the change request.</param>
public class ChangePasswordRequest(ChangePasswordDto changePasswordDto) : IRequest<ChangePasswordResponse>
{
    /// <summary>
    /// Gets the information for the change password request.
    /// </summary>
    public ChangePasswordDto ChangePasswordDto { get; } = changePasswordDto;
}