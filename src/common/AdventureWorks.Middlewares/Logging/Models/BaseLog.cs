using System.Globalization;

namespace AdventureWorks.Middlewares.Logging.Models;

/// <summary>
/// Represents the base log structure for capturing request and response details.
/// </summary>
public abstract class BaseLog
{
    /// <summary>
    /// Gets or sets the unique identifier for the request.
    /// </summary>
    /// <example>c8514258-234f-45e3-af42-581ec4199e04</example>
    public string? RequestId { get; set; }

    /// <summary>
    /// Gets or sets the URI scheme (e.g., HTTP or HTTPS).
    /// </summary>
    /// <example>https</example>
    public string? Scheme { get; set; }

    /// <summary>
    /// Gets or sets the host of the request.
    /// </summary>
    /// <example>www.xyz.com</example>
    public string? Host { get; set; }

    /// <summary>
    /// Gets or sets the path of the request.
    /// </summary>
    /// <example>/api/user</example>
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the HTTP method (e.g., GET, POST).
    /// </summary>
    /// <example>GET</example>
    public string? Method { get; set; }

    /// <summary>
    /// Gets or sets the query string of the request.
    /// </summary>
    /// <example>?pageNumber=1</example>
    public string? Query { get; set; }

    /// <summary>
    /// Gets or sets the headers of the request.
    /// </summary>
    /// <example>["Accept = application/json"]</example>
    public List<string>? Headers { get; set; }

    /// <summary>
    /// Gets or sets the cookies of the request.
    /// </summary>
    /// <example>["SessionId = xyz123"]</example>
    public List<Pairs>? Cookie { get; set; }

    /// <summary>
    /// Gets or sets the content type of the request.
    /// </summary>
    /// <example>application/json</example>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the remote IP address of the client making the request.
    /// </summary>
    /// <example>127.0.0.1</example>
    public string? RemoteIpAddress { get; set; }

    /// <summary>
    /// Gets or sets the body content of the request.
    /// </summary>
    /// <example>body: "{\"key\": \"value\"}</example>
    public string? Body { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the log creation in the current culture's format.
    /// </summary>
    /// <example>12/27/2024 10:36:01</example>
    public string Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who made the request.
    /// </summary>
    /// <example>jane.doe@xyz.com</example>
    public string? RequestedBy { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseLog"/> class with the provided request details.
    /// </summary>
    /// <param name="requestId">The unique request ID.</param>
    /// <param name="scheme">The URI scheme (e.g., HTTP or HTTPS).</param>
    /// <param name="host">The host of the request.</param>
    /// <param name="path">The path of the request.</param>
    /// <param name="method">The HTTP method (e.g., GET, POST).</param>
    /// <param name="query">The query string of the request.</param>
    /// <param name="headers">The headers of the request as a dictionary.</param>
    /// <param name="cookie">The cookies of the request as a dictionary.</param>
    /// <param name="contentType">The content type of the request.</param>
    /// <param name="remoteIpAddress">The remote IP address of the client.</param>
    /// <param name="body">The body content of the request.</param>
    /// <param name="requestedBy">The identifier of the user making the request.</param>
    /// <example>
    /// <code>
    /// var log = new DerivedLog(
    ///     requestId: "12345",
    ///     scheme: "https",
    ///     host: "example.com",
    ///     path: "/api/resource",
    ///     method: "POST",
    ///     query: "?id=1",
    ///     headers: new Dictionary<string, StringValues> { { "Authorization", "Bearer token" } },
    ///     cookie: new Dictionary<string, string> { { "sessionId", "xyz123" } },
    ///     contentType: "application/json",
    ///     remoteIpAddress: "192.168.1.1",
    ///     body: "{\"key\": \"value\"}",
    ///     requestedBy: "user@example.com"
    /// );
    /// </code>
    /// </example>
    protected BaseLog(string? requestId,
                      string? scheme,
                      string? host,
                      string? path,
                      string? method,
                      string? query,
                      IDictionary<string, StringValues>? headers,
                      IDictionary<string, string>? cookie,
                      string? contentType,
                      string? remoteIpAddress,
                      string? body,
                      string? requestedBy)
    {
        RequestId = requestId;
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
        RequestedBy = requestedBy;
    }
}

/// <summary>
/// Represents a key-value pair used for storing cookies.
/// </summary>
public class Pairs
{
    /// <summary>
    /// Gets or sets the key of the pair.
    /// </summary>
    internal string? Key { get; set; }

    /// <summary>
    /// Gets or sets the value of the pair.
    /// </summary>
    internal string? Value { get; set; }
}