using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Application.Features.Login.Request;

public class PostLoginRequest(AuthenticationDto authenticationDto) : IRequest<PostLoginResponse>
{
    internal AuthenticationDto? AuthenticationDto { get; } = authenticationDto;
}