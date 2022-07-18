namespace AdventureWorks.Sales.API.Filters;

/// <inheritdoc />
public class RequestHeaderFilter : IActionFilter
{
    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
        string remoteIpAddress = string.Empty;
        if (context.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            remoteIpAddress = context.HttpContext.Request.Headers["X-Forwarded-For"];
        context.HttpContext.Items.Add("RemoteIpAddress", remoteIpAddress);
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}