using System.Net;
using AdventureWorks.Common.Response;
using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Identity.Application.Features.Account.Command;
using Identity.Application.Features.Account.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Identity.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator ??
                    throw new Exception("Argument null exception", new ArgumentNullException(nameof(mediator)));
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(BaseResponse<LoginDto>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<LoginDto>), (int) HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<BaseResponse<LoginDto>>> Login([FromBody] AuthenticationDto authenticationDto)
    {
        BaseResponse<LoginDto> response = await _mediator.Send(new LoginQuery(authenticationDto));
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return Unauthorized(response);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<BaseResponse<UserDto>>> Register([FromBody] RegistrationDto registrationDto)
    {
        BaseResponse<UserDto> response = await _mediator.Send(new RegisterUserCommand(registrationDto));
        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);
        return Created(string.Empty, response);
    }
}