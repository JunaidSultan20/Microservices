namespace AdventureWorks.Common.Services;

public interface IUrlService
{
    string GetCurrentRequestUrl();
}

public class UrlService : IUrlService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UrlService(IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor, nameof(httpContextAccessor));

        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentRequestUrl()
    {
        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(Constants.Constants.ForwardedFor, out var remoteIpAddress))
            remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers[Constants.Constants.ForwardedFor].ToString();

        return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}{_httpContextAccessor.HttpContext.Request.Path}";
    }
}