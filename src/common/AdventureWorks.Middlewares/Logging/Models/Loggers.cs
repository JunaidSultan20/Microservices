namespace AdventureWorks.Middlewares.Logging.Models;

public class SalesLogs : BaseLog
{
    public SalesLogs(string? scheme, string? host, string? path, string? method, string? query, IDictionary<string, StringValues>? headers,
                     IDictionary<string, string>? cookie, string? contentType, string? remoteIpAddress, string? body)
        : base(scheme, host, path, method, query, headers, cookie, contentType, remoteIpAddress, body)
    {
    }
}

public class ProductionLogs : BaseLog
{
    public ProductionLogs(string? scheme, string? host, string? path, string? method, string? query, IDictionary<string, StringValues>? headers,
                          IDictionary<string, string>? cookie, string? contentType, string? remoteIpAddress, string? body)
        : base(scheme, host, path, method, query, headers, cookie, contentType, remoteIpAddress, body)
    {
    }
}