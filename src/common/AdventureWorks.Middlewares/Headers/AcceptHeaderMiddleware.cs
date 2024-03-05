namespace AdventureWorks.Middlewares.Headers;

public class AcceptHeaderMiddleware(RequestDelegate next, ILogger<AcceptHeaderMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue("Accept", out StringValues value);

        bool isValid = MediaTypeHeaderValue.TryParse(input: value, parsedValue: out _);

        if (!isValid)
        {
            ApiResult result = new ApiResult(HttpStatusCode.BadRequest, message: Messages.InvalidMediaType);

            logger.LogError(message: JsonConvert.SerializeObject(result));

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Response.ContentType = Constants.ContentTypeJson;

            await context.Response.WriteAsync(text: JsonConvert.SerializeObject(result));

            return;
        }

        await next(context);
    }
}