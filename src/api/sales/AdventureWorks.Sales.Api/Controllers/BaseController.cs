namespace AdventureWorks.Sales.Api.Controllers;

/// <summary>
/// Base controller for initializing the common fields and instances
/// </summary>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="logger"></param>
/// <exception cref="Exception"></exception>
/// <typeparam name="TController"></typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<TController>(IMediator mediator, IHttpContextAccessor httpContextAccessor, ILogger<TController> logger) : ControllerBase where TController : BaseController<TController>
{
    /// <summary>
    /// IMediator instance
    /// </summary>
    protected readonly IMediator Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), Messages.ArgumentNullExceptionMessage);

    /// <summary>
    /// IHttpContextAccessor instance
    /// </summary>
    protected readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor), Messages.ArgumentNullExceptionMessage);

    /// <summary>
    /// ILogger instance of type TController
    /// </summary>
    protected readonly ILogger<TController> Logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.ArgumentNullExceptionMessage);

    /// <summary>
    /// Client Ip Address property
    /// </summary>
    protected string? RemoteIpAddress 
    {
        get
        {
            httpContextAccessor.HttpContext.Request.Headers.TryGetValue(Constants.ForwardedFor, out var header);
            return header;
        }
    }
}