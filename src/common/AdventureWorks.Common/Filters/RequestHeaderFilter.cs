namespace AdventureWorks.Common.Filters;

/// <summary>
/// Action filter that extracts the remote IP address from the request headers and adds it to the HTTP context items.
/// </summary>
public class RequestHeaderFilter : IActionFilter
{
    /// <summary>
    /// This method is called before the action method is executed.
    /// It checks for the "Forwarded-For" header and adds the remote IP address to the HTTP context items.
    /// </summary>
    /// <param name="context">The action executing context, containing information about the current HTTP request and action.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(Constants.Constants.ForwardedFor, out var remoteIpAddress))
            remoteIpAddress = context.HttpContext.Request.Headers[Constants.Constants.ForwardedFor];
        context.HttpContext.Items.Add(Constants.Constants.RemoteIpAddress, remoteIpAddress);
    }

    /// <summary>
    /// This method is called after the action method has executed.
    /// Currently, it does not perform any operations.
    /// </summary>
    /// <param name="context">The action executed context, containing information about the HTTP response and the action result.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}