using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Application.Features.Login.Request;

/// <summary>
/// Represents the request for a post-login operation.
/// </summary>
/// <remarks>
/// This class implements the IRequest interface and defines the request for a post-login operation.
/// It includes an AuthenticationDto property to hold the authentication information.
/// </remarks>
/// <param name="authenticationDto">The authentication information for the login request.</param>
public class PostLoginRequest(AuthenticationDto authenticationDto) : IRequest<PostLoginResponse>
{
    /// <summary>
    /// Gets the authentication information for the login request.
    /// </summary>
    //public AuthenticationDto? AuthenticationDto { get; } = authenticationDto;

    public string Email => authenticationDto.Email;
    public string Password => authenticationDto.Password;
}