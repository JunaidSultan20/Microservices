using AdventureWorks.Identity.Application.Dto;
using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;
using AdventureWorks.Identity.Application.Features.RefreshToken.Request;
using AdventureWorks.Identity.Application.Features.RefreshToken.Response;
using AdventureWorks.Identity.Application.Features.Register.Request;
using AdventureWorks.Identity.Application.Features.Register.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AdventureWorks.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]", Name = "Login")]
    [ProducesResponseType(typeof(PostLoginResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(PostUnauthorizedAttemptResponse), (int)HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<PostLoginResponse>> Login([FromBody] AuthenticationDto authenticationDto, 
                                                             CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(argument: authenticationDto, paramName: nameof(authenticationDto));

        var response = await _mediator.Send(new PostLoginRequest(authenticationDto), cancellationToken);

        if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            return Unauthorized(response);

        return Ok(response);
    }

    [HttpPost(template: "[action]", Name = "Register")]
    [ProducesResponseType(typeof(PostRegisterResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BadRequestPostRegisterResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ConflictPostRegisterResponse), (int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<PostRegisterResponse>> Register([FromBody] RegistrationDto registrationDto,
                                                                   CancellationToken cancellationToken = default)
    {
        PostRegisterResponse response = await _mediator.Send(request: new PostRegisterRequest(registrationDto),
                                                             cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => BadRequest(response)
        };
    }

    [HttpGet(template: "[action]", Name = "Refresh")]
    [ProducesResponseType(typeof(RefreshTokenResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ForbiddenRefreshTokenResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(NotFoundRefreshTokenResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(UnauthorizedRefreshTokenResponse), (int)HttpStatusCode.Unauthorized)]

    public async Task<ActionResult<RefreshTokenResponse>> Refresh(CancellationToken cancellationToken = default)
    {
        RefreshTokenResponse response = await _mediator.Send(new RefreshTokenRequest(), cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(response),
            HttpStatusCode.Forbidden => StatusCode((int)HttpStatusCode.Forbidden, response),
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.Unauthorized => Unauthorized(response),
            _ => BadRequest(response)
        };
    }
}