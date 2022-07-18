namespace AdventureWorks.Sales.API.Controllers;

/// <inheritdoc />
[Route("api")]
[ApiController]
public class RootController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <inheritdoc />
    public RootController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Root method to return url for all endpoints in the API.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetRoot")]
    public async Task<ActionResult<BaseResponse<IReadOnlyList<Links>>>> GetRoot()
    {
        string remoteIpAddress = string.Empty;
        if (_httpContextAccessor.HttpContext is not null &&
            _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];

        return Ok(await Task.Run(() =>
        {
            List<Links> links = new List<Links>
            {
                new Links(Url.RouteUrl("GetRoot")?
                    .Replace("/api", $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{remoteIpAddress}/gateway"), 
                    "self", "GET"),
                
                new Links(Url.RouteUrl("GetCustomerList", new { pageNumber = 1, pageSize = 10, ver = 1.0 })?
                    .Replace("/api", $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{remoteIpAddress}/gateway"), 
                    "customer", "GET"),
                
                new Links(Url.RouteUrl("GetCustomerById", new { id = 1 })?
                    .Replace("/api", $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{remoteIpAddress}/gateway"), 
                    "customer", "GET"),
                
                new Links(Url.RouteUrl("GetCustomerByIdRange", new {minId = 1, maxId = 10})?
                    .Replace("/api", $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{remoteIpAddress}/gateway"), 
                    "customer", "GET"),
                
                new Links(Url.RouteUrl("UpdateCustomerById", new { id = 1 })?
                    .Replace("/api", $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{remoteIpAddress}/gateway"), 
                    "update_customer", "PUT")
            };
            return new BaseResponse<IReadOnlyList<Links>>(HttpStatusCode.OK, null, links.AsReadOnly());
        }));
    }
}