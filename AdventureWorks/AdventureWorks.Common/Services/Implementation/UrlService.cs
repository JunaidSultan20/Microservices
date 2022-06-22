using AdventureWorks.Common.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace AdventureWorks.Common.Services.Implementation;

public class UrlService : IUrlService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UrlService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentRequestUrl()
    {
        return
            $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}";
    }
}