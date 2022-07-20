namespace AdventureWorks.Sales.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected string? RemoteIpAddress { get; }

    public BaseController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator ??
                    throw new Exception("Argument null exception", new ArgumentNullException(nameof(mediator)));
        _httpContextAccessor = httpContextAccessor ?? throw new Exception("Argument null exception",
            new ArgumentNullException(nameof(httpContextAccessor)));
        if (_httpContextAccessor.HttpContext is not null &&
            _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            RemoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
    }
}