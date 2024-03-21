using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Globalization;

namespace AdventureWorks.Middlewares.Logging;

public class RequestLoggingMiddleware(RequestDelegate next, 
                                      IMongoClient client, 
                                      IOptionsMonitor<RequestLogOptions> requestLogOptions)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await LogRecord(context);

        await next(context);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, 
                                            Encoding.UTF8, 
                                            true, 
                                            1024, 
                                            true);

        var body = await reader.ReadToEndAsync();

        request.Body.Position = 0;

        return body;
    }

    private async Task LogRecord(HttpContext context)
    {
        var request = context.Request;

        var requestBody = await ReadRequestBody(request);

        BsonDocument log = new BsonDocument
        {
            { "scheme", request.Scheme },
            { "host", request.Host.ToString() },
            { "path", request.Path.ToString() },
            { "method", request.Method },
            { "query", request.QueryString.ToString() },
            { "headers", new BsonArray(request.Headers?.Select(x => string.Join(" = ", x.Key, x.Value)).ToArray()) },
            { "cookies", new BsonArray(request.Cookies.ToDictionary().Select(x => new Pairs { Key = x.Key, Value = x.Value }).ToArray()) },
            { "contentType", request.ContentType },
            { "remoteIpAddress", context.Connection.RemoteIpAddress?.ToString() },
            { "body", requestBody },
            { "timestamp", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
        };

        IMongoDatabase database = client.GetDatabase(requestLogOptions.CurrentValue.Database);

        await database.GetCollection<BsonDocument>(requestLogOptions.CurrentValue.Collection).InsertOneAsync(log);
    }
}