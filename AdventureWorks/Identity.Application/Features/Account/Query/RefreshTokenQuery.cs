using AdventureWorks.Common.Response;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Account.Query;

public class RefreshTokenQuery : IRequest<BaseResponse<RefreshTokenDto>>
{
    internal RefreshTokenDto? RefreshTokenDto { get; set; }

    public RefreshTokenQuery(RefreshTokenDto refreshTokenDto)
    {
        RefreshTokenDto = refreshTokenDto;
    }
}