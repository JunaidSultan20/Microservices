using AdventureWorks.Common.Response;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Account.Query;

public class LoginQuery : IRequest<BaseResponse<LoginDto>>
{
    internal AuthenticationDto? AuthenticationDto { get; }

    public LoginQuery(AuthenticationDto authenticationDto)
    {
        AuthenticationDto = authenticationDto;
    }
}