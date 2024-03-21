namespace AdventureWorks.Middlewares.Exceptions;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ExceptionMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError($"An error occurred: {exception}");

            context.Response.ContentType = Constants.ContentTypeJson;

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // You can customize the error response JSON as needed.
            ApiException exceptionResponse = new ApiException("An error occurred while processing your request.",
                                                              exception.Message,
                                                              exception.InnerException?.Message ?? string.Empty,
                                                              exception.StackTrace);

            // Serialize the error response and write it to the response stream.
            await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionResponse));
        }
    }
}