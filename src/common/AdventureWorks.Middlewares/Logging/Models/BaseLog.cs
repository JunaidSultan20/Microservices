using System.Globalization;

namespace AdventureWorks.Middlewares.Logging.Models;

public abstract class BaseLog
{
    public string? Scheme { get; set; }
    
    public string? Host { get; set; }
    
    public string? Path { get; set; }
    
    public string? Method { get; set; }
    
    public string? Query { get; set; }
    
    public List<string>? Headers { get; set; }
    
    public List<Pairs>? Cookie { get; set; }
    
    public string? ContentType { get; set; }
    
    public string? RemoteIpAddress { get; set; }
    
    public string? Body { get; set; }
    
    public string Timestamp { get; set; }

    protected BaseLog(string? scheme,
                      string? host,
                      string? path,
                      string? method,
                      string? query,
                      IDictionary<string, StringValues>? headers,
                      IDictionary<string, string>? cookie,
                      string? contentType,
                      string? remoteIpAddress,
                      string? body)
    {
        Scheme = scheme;

        Host = host;

        Path = path;

        Method = method;

        Query = query;

        Headers = headers?.Select(x => string.Join(" = ", x.Key, x.Value)).ToList();

        Cookie = cookie?.Select(x => new Pairs { Key = x.Key, Value = x.Value })
                        .ToList();

        ContentType = contentType;

        RemoteIpAddress = remoteIpAddress;

        Body = body;

        Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
    }
}

public class Pairs
{
    internal string? Key { get; set; }

    internal string? Value { get; set; }
}