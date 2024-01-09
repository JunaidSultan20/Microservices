namespace AdventureWorks.Common.Filters;

public class RequestHeaderFilter : IActionFilter
{
    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(Constants.Constants.ForwardedFor, out var remoteIpAddress))
            remoteIpAddress = context.HttpContext.Request.Headers[Constants.Constants.ForwardedFor];
        context.HttpContext.Items.Add(Constants.Constants.RemoteIpAddress, remoteIpAddress);
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}