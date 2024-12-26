namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents a hypermedia link with URL, relation type, and HTTP method.
/// </summary>
public record Links
{
    /// <summary>
    /// Gets or sets the URL of the link.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// Gets the relation type of the link, indicating the purpose of the link.
    /// </summary>
    public string? Rel { get; }

    /// <summary>
    /// Gets the HTTP method used to interact with the link.
    /// </summary>
    public string? Method { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Links"/> record.
    /// </summary>
    private Links() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Links"/> record with specified URL, relation type, and HTTP method.
    /// </summary>
    /// <param name="href">The URL of the link.</param>
    /// <param name="rel">The relation type of the link.</param>
    /// <param name="method">The HTTP method for the link.</param>
    public Links(string? href, 
                 string? rel, 
                 string? method) 
        => (Href, Rel, Method) = (href, rel, method);

    /// <summary>
    /// Creates a new instance of the <see cref="Links"/> record with default values.
    /// </summary>
    /// <returns>A new instance of the <see cref="Links"/> record.</returns>
    public static Links CreateLink()
    {
        return new Links();
    }

    /// <summary>
    /// Sets the properties of the link based on provided parameters.
    /// </summary>
    /// <param name="urlHelper">The URL helper used to generate the link URL.</param>
    /// <param name="routeName">The name of the route to generate the link URL.</param>
    /// <param name="routeValues">The route values to use for generating the link URL.</param>
    /// <param name="rel">The relation type of the link.</param>
    /// <param name="method">The HTTP method for the link.</param>
    /// <param name="scheme">The scheme to use for the link URL.</param>
    /// <param name="remoteIpAddress">The remote IP address used to generate the link URL.</param>
    /// <returns>The updated <see cref="Links"/> instance.</returns>
    public Links SetLink(IUrlHelper urlHelper,
                         string? routeName,
                         object? routeValues,
                         string? rel,
                         string? method,
                         string? scheme,
                         string? remoteIpAddress)
    {
        Href = urlHelper.RouteUrl(routeName, routeValues)?.Replace("/api", $"{scheme}://{remoteIpAddress}/api");
        //Rel = rel;        
        //Method = method;        
        return this;
    }
}