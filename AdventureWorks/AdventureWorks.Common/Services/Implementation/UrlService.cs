using AdventureWorks.Common.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.Common.Services.Implementation;

public class UrlService : IUrlService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;

    public UrlService(IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
    }

    public string GetCurrentRequestUrl()
    {
        string remoteIpAddress = string.Empty;
        if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];

        //if (_hostEnvironment.IsDevelopment())
        //{
        //    return
        //        $"{_httpContextAccessor.HttpContext.Request.Scheme}://localhost:5000{_httpContextAccessor.HttpContext.Request.Path}";
        //}
        //return
        //    $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}";
        return
            $"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}{_httpContextAccessor.HttpContext.Request.Path.Value.Replace("/api", "/gateway")}";
    }
}