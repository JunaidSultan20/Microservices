namespace AdventureWorks.Sales.Api.Controllers;

/// <summary>
/// Base controller class
/// </summary>
/// <typeparam name="TController"></typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<TController> : ControllerBase where TController : BaseController<TController>
{
    /// <summary>
    /// IMediator instance
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// IHttpContextAccessor instance
    /// </summary>
    protected readonly IHttpContextAccessor HttpContextAccessor;

    /// <summary>
    /// ILogger instance of type TController
    /// </summary>
    protected readonly ILogger<TController> Logger;

    /// <summary>
    /// Client Ip Address property
    /// </summary>
    protected string? RemoteIpAddress { get; }

    /// <summary>
    /// Base controller for initializing the common fields and instances
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="logger"></param>
    /// <exception cref="Exception"></exception>
    protected BaseController(IMediator mediator, IHttpContextAccessor httpContextAccessor, ILogger<TController> logger)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), Messages.ArgumentNullExceptionMessage);

        HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor), Messages.ArgumentNullExceptionMessage);

        Logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.ArgumentNullExceptionMessage);

        if (HttpContextAccessor.HttpContext is not null &&
            HttpContextAccessor.HttpContext.Request.Headers.TryGetValue(key: Constants.ForwardedFor, value: out var header))
            RemoteIpAddress = header;
    }
}