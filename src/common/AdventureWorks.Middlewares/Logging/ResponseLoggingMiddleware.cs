namespace AdventureWorks.Middlewares.Logging;

public class ResponseLoggingMiddleware(RequestDelegate next, IConfiguration configuration, ServiceName serviceName)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ServiceName _serviceName = serviceName;

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the response details
        var response = context.Response;

        var responseBody = await ReadResponseBody(response);

        //var mongoClient = new MongoClient(_configuration.GetValue<string>("RequestLogDbConfig:ServerUri"));

        //IMongoDatabase database = mongoClient.GetDatabase(_configuration.GetValue<string>("RequestLogDbConfig:Database"));

        //string collectionName = GetCollectionName(_serviceName);

        //var collection = database.GetCollection<Loggers>(collectionName);

        //Loggers log = new Loggers(context.Request.Scheme,
        //                                  context.Request.Host.ToString(),
        //                                  context.Request.Path.ToString(),
        //                                  context.Request.Method,
        //                                  context.Request.QueryString.ToString(),
        //                                  context.Request.Headers,
        //                                  context.Request.Cookies.ToDictionary(),
        //                                  context.Request.ContentType,
        //                                  context.Connection.RemoteIpAddress?.ToString(),
        //                                  responseBody);

        //await collection.InsertOneAsync(log);

        await next(context);
    }

    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin); // Reset the position so that the response can be sent to the client
        return body;
    }

    private string GetCollectionName(ServiceName serviceName)
    {
        return serviceName switch
        {
            ServiceName.Sales => "sales_logs",
            ServiceName.Production => "production_logs",
            _ => string.Empty
        };
    }
}