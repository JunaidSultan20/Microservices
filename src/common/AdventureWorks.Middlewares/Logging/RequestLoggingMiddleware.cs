using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Globalization;
using AdventureWorks.Common.Helpers;

namespace AdventureWorks.Middlewares.Logging;

public class RequestLoggingMiddleware(RequestDelegate next,
                                      IMongoClient client,
                                      IOptionsMonitor<RequestLogOptions> options,
                                      bool logBody = true)
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
        string requestBody = logBody ? await ReadRequestBody(request) : string.Empty;
        BsonDocument log = new BsonDocument
        {
            { "requestId", context.Items[Constants.RequestId]?.ToString() ?? string.Empty },
            { "scheme", request.Scheme },
            { "host", request.Host.ToString() },
            { "path", request.Path.ToString() },
            { "method", request.Method },
            { "query", request.QueryString.ToString() },
            { "headers", request.Headers.Any()
                    ? new BsonArray(request.Headers?
                                           .Where(x => 
                                                          !string.Equals(x.Key, Constants.Authorization, StringComparison.OrdinalIgnoreCase) && 
                                                          !string.Equals(x.Key, Constants.Cookie, StringComparison.OrdinalIgnoreCase))
                                           .Select(x => string.Join(" = ", x.Key, x.Value))
                                           .ToArray())
                    : new BsonArray() },
            { "cookies", request.Cookies.Any()
    ? new BsonArray(
        request.Cookies
            .Where(x => !string.Equals(x.Key, Constants.BearerToken, StringComparison.OrdinalIgnoreCase))
            .Select(x => string.Join(" = ", x.Key, x.Value))
    )
    : new BsonArray() }, 
            { "contentType", request.ContentType ?? string.Empty }, 
            { "remoteIpAddress", request.Headers?[Constants.ForwardedFor].ToString() ?? string.Empty }, 
            { "body", requestBody }, 
            { "timestamp", DateTime.Now.ToString(CultureInfo.InvariantCulture) }, 
            { "requestedBy", request.Headers?.ContainsKey(Constants.Authorization) == true 
                    ? TokenHelper.GetUserEmail(request.Headers?[Constants.Authorization]
                                                      .ToString()
                                                      .Substring("Bearer ".Length)
                                                      .Trim() ?? string.Empty) 
                    : string.Empty }
        };
        IMongoDatabase database = client.GetDatabase(options.CurrentValue.Database);
        await database.GetCollection<BsonDocument>(options.CurrentValue.Collection).InsertOneAsync(log);
    }
}