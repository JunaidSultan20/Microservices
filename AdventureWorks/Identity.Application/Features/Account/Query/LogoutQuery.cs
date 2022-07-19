using AdventureWorks.Common.Response;
using MediatR;

namespace Identity.Application.Features.Account.Query;

public class LogoutQuery : IRequest<BaseResponse<object>>
{
    public string? Token { get; set; }

    public LogoutQuery(string token)
    {
        Token = token;
    }
}