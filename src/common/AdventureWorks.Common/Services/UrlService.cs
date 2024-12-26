namespace AdventureWorks.Common.Services;

/// <summary>
/// Defines a service for retrieving the current request URL.
/// </summary>
public interface IUrlService
{
    /// <summary>
    /// Gets the URL of the current request.
    /// </summary>
    /// <returns>The current request URL as a string.</returns>
    string GetCurrentRequestUrl();
}

/// <summary>
/// Provides a service for retrieving the URL of the current request.
/// </summary>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/> instance used to access the HTTP context.</param>
public class UrlService(IHttpContextAccessor httpContextAccessor) : IUrlService
{
    /// <summary>
    /// Gets the URL of the current request, including scheme, remote IP address, and request path.
    /// </summary>
    /// <returns>
    /// A string representing the current request URL, constructed from the request scheme, remote IP address (if available), and request path.
    /// </returns>
    /// <remarks>
    /// If the <see cref="IHttpContextAccessor"/> is used to obtain the remote IP address from the request headers,
    /// it will be included in the URL if available. Otherwise, the URL will include only the scheme and request path.
    /// </remarks>
    public string GetCurrentRequestUrl()
    {
        if (httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(Constants.Constants.ForwardedFor, out var remoteIpAddress))
            remoteIpAddress = httpContextAccessor.HttpContext.Request.Headers[Constants.Constants.ForwardedFor].ToString();

        return $"{httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}{httpContextAccessor.HttpContext.Request.Path}";
    }
}