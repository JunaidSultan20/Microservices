namespace AdventureWorks.Middlewares.Exceptions;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError($"An error occurred: {exception}");
            context.Response.ContentType = Constants.ContentTypeJson;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ApiException exceptionResponse = new ApiException("An error occurred while processing your request",
                                                              exception.Message,
                                                              exception.InnerException?.Message ?? string.Empty,
                                                              exception.StackTrace);

            // Serialize the error response and write it to the response stream.
            await context.Response.WriteAsync(text: JsonConvert.SerializeObject(exceptionResponse));
            context.Abort();
        }
    }
}