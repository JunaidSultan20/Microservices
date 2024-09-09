using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Globalization;

namespace AdventureWorks.Middlewares.Logging;

public class RequestLoggingMiddleware(RequestDelegate next, 
                                      IMongoClient client, 
                                      IOptionsMonitor<RequestLogOptions> options)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await LogRecord(context);
        await next(context);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true);
        string body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return body;
    }

    private async Task LogRecord(HttpContext context)
    {
        HttpRequest request = context.Request;
        string requestBody = await ReadRequestBody(request);
        BsonDocument log = new BsonDocument
        {
            { "scheme", request.Scheme ?? string.Empty },
            { "host", request.Host.ToString() ?? string.Empty },
            { "path", request.Path.ToString() ?? string.Empty },
            { "method", request.Method ?? string.Empty },
            { "query", request.QueryString.ToString() ?? string.Empty },
            { "headers", request.Headers != null 
                    ? new BsonArray(request.Headers?.Select(x => string.Join(" = ", x.Key, x.Value)).ToArray()) 
                    : new BsonArray() },
            { "cookies", request.Cookies != null 
                    ? new BsonArray(request.Cookies
                                           .Where(x => !string.Equals(x.Key, "bearerToken", StringComparison.OrdinalIgnoreCase)) // Exclude "bearerToken"
                                           .Select(x => new Pairs { Key = x.Key, Value = x.Value }).ToArray())
                    : new BsonArray() },
            { "contentType", request.ContentType ?? string.Empty },
            { "remoteIpAddress", context.Connection.RemoteIpAddress?.ToString() ?? string.Empty },
            { "body", requestBody ?? string.Empty },
            { "timestamp", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
        };
        IMongoDatabase database = client.GetDatabase(options.CurrentValue.Database);
        await database.GetCollection<BsonDocument>(options.CurrentValue.Collection).InsertOneAsync(log);
    }
}