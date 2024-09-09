using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;
using AdventureWorks.Identity.Application.Features.RefreshToken.Request;
using AdventureWorks.Identity.Application.Features.RefreshToken.Response;
using AdventureWorks.Identity.Application.Features.Register.Request;
using AdventureWorks.Identity.Application.Features.Register.Response;

namespace AdventureWorks.Identity.Api.Controllers;

/// <summary>
/// Account controller that contains the endpoints to manage the authentication and authorization related activities.
/// </summary>
/// <param name="serviceProvider">
/// An instance of IServiceProvider that is used to resolve services.
/// </param>
public class AccountController(IServiceProvider serviceProvider) : BaseController<AccountController>(serviceProvider)
{
    /// <summary>
    /// Authenticates the user and returns a login response.
    /// </summary>
    /// <remarks>
    /// This endpoint handles user authentication by validating the provided credentials and returns a response indicating success or failure.
    /// </remarks>
    /// <param name="authenticationDto">The user credentials required for authentication.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>
    ///   <para>Returns a login response indicating whether the authentication was successful or not.</para>
    ///   <para>If successful, returns HTTP status code 200 (OK) along with the user's authentication details.</para>
    ///   <para>If the credentials are invalid, returns HTTP status code 401 (Unauthorized).</para>
    /// </returns>
    /// <response code="200">Authentication was successful.</response>
    /// <response code="401">Invalid credentials provided.</response>
    /// <example>
    ///     POST /api/account/login
    ///     Request:
    ///     {
    ///         "username": "user@example.com",
    ///         "password": "YourPassword123"
    ///     }
    ///     Response:
    ///     HTTP/1.1 200 OK
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 200,
    ///         "message": "Login successful",
    ///         "isSuccessful": true,
    ///         "result": {
    ///             "userId": 123,
    ///             "username": "user@example.com",
    ///             "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    ///             "expiresIn": 3600
    ///         }
    ///     }
    /// 
    ///     OR
    /// 
    ///     HTTP/1.1 401 Unauthorized
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 401,
    ///         "message": "Invalid username or password",
    ///         "isSuccessful": false,
    ///         "result": null
    ///     }
    /// </example>
    [HttpPost(template: "[action]", Name = "Login")]
    [SwaggerRequestExample(typeof(PostLoginRequest), typeof(PostLoginRequestExample))]
    [ProducesResponseType(typeof(PostLoginResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(PostUnauthorizedAttemptResponse), (int)HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<PostLoginResponse>> Login([FromBody] AuthenticationDto authenticationDto, 
                                                             CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(argument: authenticationDto, paramName: nameof(authenticationDto));
        PostLoginResponse response = await Mediator.Send(request: new PostLoginRequest(authenticationDto), cancellationToken: cancellationToken);

        if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            return Unauthorized(response);

        return Ok(response);
    }

    /// <summary>
    /// Registers the new user and returns the registration response.
    /// </summary>
    /// <remarks>
    /// This endpoint handles user registration by adding the new user in the system and returns the response indicating success or failure
    /// </remarks>
    /// <param name="registrationDto">The data transfer object containing user registration details.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    ///   <para>Returns the registration response indicating the user has been created successfully.</para>
    ///   <para>If successful, returns HTTP status code 201 (Created) along with the user's details.</para>
    ///   <para>If unable to create user, returns HTTP status code 400 (Bad Request).</para>
    ///   <para>If unable to assign role to user, returns HTTP status code 400 (Bad Request).</para>
    ///   <para>If unable to find role, returns HTTP status code 404 (Not Found).</para>
    ///   <para>If user already exists, returns HTTP status code 409 (Conflict).</para>
    /// </returns>
    /// <response code="201">Returns the <see cref="PostRegisterResponse"/> if the registration is successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestPostRegisterResponse"/> if the registration data is invalid.</response>
    /// <response code="409">Returns a <see cref="ConflictPostRegisterResponse"/> if the user already exists.</response>
    /// <example>
    ///     POST /api/account/register
    ///     Request:
    ///     {
    ///         "username": "jon.doe",
    ///         "email": "jon.doe@example.com",
    ///         "password": "YourPassword123"
    ///         "role": "TestRole"
    ///     }
    ///     Response:
    ///     HTTP/1.1 201 Created
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 201,
    ///         "message": "User created successfully",
    ///         "isSuccessful": true,
    ///         "result": {
    ///             "username": "jon.doe",
    ///             "email": "jon.doe@example.com"
    ///         }
    ///     }
    ///
    ///     OR
    ///
    ///     HTTP/1.1 400 Bad Request
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 400,
    ///         "message": "Unable to create user",
    ///         "isSuccessful": false,
    ///         "result": null
    ///     }
    ///
    ///     OR
    ///
    ///     HTTP/1.1 400 Bad Request
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 400,
    ///         "message": "Unable to assign role",
    ///         "isSuccessful": false,
    ///         "result": null
    ///     }
    ///
    ///     OR
    ///
    ///     HTTP/1.1 409 Conflict
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 409,
    ///         "message": "User already exists",
    ///         "isSuccessful": false,
    ///         "result": null
    ///     }
    /// </example>
    [HttpPost(template: "[action]", Name = "Register")]
    [SwaggerRequestExample(typeof(PostRegisterRequest), typeof(PostRegisterRequestExample))]
    [ProducesResponseType(typeof(PostRegisterResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BadRequestPostRegisterResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ConflictPostRegisterResponse), (int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<PostRegisterResponse>> Register([FromBody] RegistrationDto registrationDto,
                                                                   CancellationToken cancellationToken = default)
    {
        PostRegisterResponse response = await Mediator.Send(request: new PostRegisterRequest(registrationDto), cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            HttpStatusCode.BadRequest => BadRequest(response),
            HttpStatusCode.Conflict => Conflict(response),
            _ => BadRequest(response)
        };
    }

    /// <summary>
    /// Handles the refreshing of an expired authentication token.
    /// </summary>
    /// <remarks>
    /// This endpoint handles refreshing the expired access token by regenerating it and returns the refreshed token via the cookie
    /// </remarks>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    ///   <para code="200">Returns the <see cref="RefreshTokenResponse"/> if the token is refreshed successfully.</para>
    ///   <para code="401">If the access token is not present in the cookie, returns <see cref="UnauthorizedRefreshTokenResponse"/> with status code 401 (Unauthorized).</para>
    /// </returns>
    /// <response code="403">Returns a <see cref="ForbiddenRefreshTokenResponse"/> if the user is forbidden from refreshing the token.</response>
    /// <response code="404">Returns a <see cref="NotFoundRefreshTokenResponse"/> if the token or user is not found.</response>
    /// <response code="401">Returns an <see cref="UnauthorizedRefreshTokenResponse"/> if the request is unauthorized.</response>
    [HttpGet(template: "[action]", Name = nameof(Refresh))]
    [ProducesResponseType(typeof(RefreshTokenResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ForbiddenRefreshTokenResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(NotFoundRefreshTokenResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(UnauthorizedRefreshTokenResponse), (int)HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<RefreshTokenResponse>> Refresh(CancellationToken cancellationToken = default)
    {
        RefreshTokenResponse response = await Mediator.Send(request: new RefreshTokenRequest(), cancellationToken: cancellationToken);

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