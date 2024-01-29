using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Application.Features.Login.Request;

public class PostLoginRequest : IRequest<PostLoginResponse>
{
    internal AuthenticationDto? AuthenticationDto { get; }

    public PostLoginRequest(AuthenticationDto authenticationDto) => AuthenticationDto = authenticationDto;
}