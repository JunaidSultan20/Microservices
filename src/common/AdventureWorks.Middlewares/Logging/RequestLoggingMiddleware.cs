using AdventureWorks.Common.Options;

namespace AdventureWorks.Middlewares.Logging;

public class RequestLoggingMiddleware(RequestDelegate next, IMongoClient client, RequestLogOptions requestLogOptions, ServiceName serviceName)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await LogRecord(context, serviceName);

        await next(context);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);

        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return body;
    }

    private async Task LogRecord(HttpContext context, ServiceName serviceName)
    {
        var request = context.Request;

        var requestBody = await ReadRequestBody(request);



        BaseLog? log = serviceName switch
        {
            ServiceName.Sales => new SalesLogs(request.Scheme,
                                               request.Host.ToString(),
                                               request.Path.ToString(),
                                               request.Method,
                                               request.QueryString.ToString(),
                                               request.Headers,
                                               request.Cookies.ToDictionary(),
                                               request.ContentType,
                                               context.Connection.RemoteIpAddress?.ToString(),
                                               requestBody),
            ServiceName.Production => new ProductionLogs(request.Scheme,
                                                         request.Host.ToString(),
                                                         request.Path.ToString(),
                                                         request.Method,
                                                         request.QueryString.ToString(),
                                                         request.Headers,
                                                         request.Cookies.ToDictionary(),
                                                         request.ContentType,
                                                         context.Connection.RemoteIpAddress?.ToString(),
                                                         requestBody),
            _ => default
        };

        IMongoDatabase database = client.GetDatabase(requestLogOptions.Database);

        if (serviceName.Equals(ServiceName.Sales))
        {
            await database.GetCollection<SalesLogs>("sales_logs").InsertOneAsync((SalesLogs)log);
        }
        else if (serviceName.Equals(ServiceName.Production))
        {
            await database.GetCollection<ProductionLogs>("production_logs").InsertOneAsync((ProductionLogs)log);
        }


        //if (serviceName.Equals(ServiceName.Sales))
        //{
        //    SalesLogs log = new SalesLogs(context.Request.Scheme,
        //                                  context.Request.Host.ToString(),
        //                                  context.Request.Path.ToString(),
        //                                  context.Request.Method,
        //                                  context.Request.QueryString.ToString(),
        //                                  context.Request.Headers,
        //                                  context.Request.Cookies.ToDictionary(),
        //                                  context.Request.ContentType,
        //                                  context.Connection.RemoteIpAddress?.ToString(),
        //                                  requestBody);

        //    IMongoDatabase database = _client.GetDatabase(_configuration.GetValue<string>("RequestLogDbConfig:Database"));

        //    var collection = database.GetCollection<SalesLogs>("sales_logs");

        //    await collection.InsertOneAsync(log);
        //}

        //if (serviceName.Equals(ServiceName.Production))
        //{
        //    ProductionLogs log = new ProductionLogs(context.Request.Scheme,
        //                                            context.Request.Host.ToString(),
        //                                            context.Request.Path.ToString(),
        //                                            context.Request.Method,
        //                                            context.Request.QueryString.ToString(),
        //                                            context.Request.Headers,
        //                                            context.Request.Cookies.ToDictionary(),
        //                                            context.Request.ContentType,
        //                                            context.Connection.RemoteIpAddress?.ToString(),
        //                                            requestBody);

        //    IMongoDatabase database = _client.GetDatabase(_configuration.GetValue<string>("RequestLogDbConfig:Database"));

        //    var collection = database.GetCollection<ProductionLogs>("production_logs");

        //    await collection.InsertOneAsync(log);
        //}
    }
}