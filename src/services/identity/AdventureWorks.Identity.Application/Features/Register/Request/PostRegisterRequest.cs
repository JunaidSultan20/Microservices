using AdventureWorks.Identity.Application.Features.Register.Response;

namespace AdventureWorks.Identity.Application.Features.Register.Request;

public class PostRegisterRequest : IRequest<PostRegisterResponse>
{
    internal RegistrationDto RegistrationDto { get; }

    public PostRegisterRequest(RegistrationDto registrationDto) => RegistrationDto = registrationDto;
}