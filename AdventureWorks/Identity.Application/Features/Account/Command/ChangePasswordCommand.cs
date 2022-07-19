using AdventureWorks.Common.Response;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Account.Command;

public class ChangePasswordCommand : IRequest<BaseResponse<object>>
{
    public ChangePasswordDto? ChangePasswordDto { get; set; }
}