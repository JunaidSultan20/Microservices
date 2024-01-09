namespace AdventureWorks.Sales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController<TController> : ControllerBase where TController : BaseController<TController>
{
    protected readonly IMediator Mediator;
    protected readonly IHttpContextAccessor HttpContextAccessor;
    protected readonly ILogger<TController> Logger;
    protected string? RemoteIpAddress { get; }

    public BaseController(IMediator mediator, IHttpContextAccessor httpContextAccessor, ILogger<TController> logger)
    {
        Mediator = mediator ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage,
                                                   innerException: new ArgumentNullException(nameof(mediator)));

        HttpContextAccessor = httpContextAccessor ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage,
                                                                         innerException: new ArgumentNullException(nameof(httpContextAccessor)));

        Logger = logger ?? throw new Exception(message: Messages.ArgumentNullExceptionMessage,
                                               innerException: new ArgumentNullException(nameof(httpContextAccessor)));

        if (HttpContextAccessor.HttpContext is not null &&
            HttpContextAccessor.HttpContext.Request.Headers.TryGetValue(key: Constants.ForwardedFor, value: out var header))
            RemoteIpAddress = header;
    }
}