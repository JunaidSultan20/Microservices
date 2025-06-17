namespace AdventureWorks.Middlewares.RequestId;

public class RequestIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        string requestId = Guid.NewGuid().ToString();
        context.Items[Constants.RequestId] = requestId;
        context.Response.Headers[Constants.RequestId] = requestId;
        await next(context);
    }
}