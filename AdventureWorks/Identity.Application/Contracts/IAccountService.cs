using AdventureWorks.Common.Response;
using Identity.Application.DTOs;

namespace Identity.Application.Contracts;

public interface IAccountService
{
    Task<BaseResponse<LoginDto>> LoginAsync(AuthenticationDto authenticationDto);

    Task<BaseResponse<UserDto>> RegisterAsync(RegistrationDto registrationDto);

    Task<BaseResponse<RefreshTokenDto>> RefreshToken(RefreshTokenDto refreshTokenDto);

    Task<BaseResponse<object>> LogoutAsync(string token);

    Task<BaseResponse<object>> ChangePassword(string email, string currentPassword, string newPassword);

    Task AddRole(string roleName);
}