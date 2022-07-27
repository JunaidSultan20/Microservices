namespace AdventureWorks.Sales.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;
    protected readonly IHttpContextAccessor HttpContextAccessor;
    protected string? RemoteIpAddress { get; }

    public BaseController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        Mediator = mediator ??
                    throw new Exception("Argument null exception", new ArgumentNullException(nameof(mediator)));
        HttpContextAccessor = httpContextAccessor ?? throw new Exception("Argument null exception",
            new ArgumentNullException(nameof(httpContextAccessor)));
        if (HttpContextAccessor.HttpContext is not null &&
            HttpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            RemoteIpAddress = HttpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
    }
}