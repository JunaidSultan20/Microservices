namespace AdventureWorks.Common.Services;

public interface IUrlService
{
    string GetCurrentRequestUrl();
}

public class UrlService(IHttpContextAccessor httpContextAccessor) : IUrlService
{
    public string GetCurrentRequestUrl()
    {
        if (httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(Constants.Constants.ForwardedFor, out var remoteIpAddress))
            remoteIpAddress = httpContextAccessor.HttpContext.Request.Headers[Constants.Constants.ForwardedFor].ToString();

        return $"{httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}{httpContextAccessor.HttpContext.Request.Path}";
    }
}