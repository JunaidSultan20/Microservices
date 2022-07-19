using AdventureWorks.Common.Response;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Account.Command;

public class RegisterUserCommand : IRequest<BaseResponse<UserDto>>
{
    internal RegistrationDto? RegistrationDto { get; }

    public RegisterUserCommand(RegistrationDto registrationDto)
    {
        RegistrationDto = registrationDto;
    }
}