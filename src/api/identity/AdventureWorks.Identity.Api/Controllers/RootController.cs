using AdventureWorks.Common.Response;
using Microsoft.Extensions.Primitives;

namespace AdventureWorks.Identity.Api.Controllers;

/// <summary>
/// Root controller that contains the endpoints to provide the details about each endpoint present in the api.
/// </summary>
/// <param name="serviceProvider">
/// An instance of IServiceProvider that is used to resolve services.
/// </param>
[Produces(contentType: Constants.ContentTypeJson)]
public class RootController(IServiceProvider serviceProvider) : BaseController<RootController>(serviceProvider)
{
    /// <summary>
    /// Retrieves the root information, including a list of all registered API endpoints and their details.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a collection of links representing the available routes in the application,
    /// including their HTTP methods and names. It also captures the remote IP address from the request headers
    /// if available.
    /// </remarks>
    /// <returns>
    ///   <para>Returns a <see cref="RootResponse"/> containing a list of <see cref="Links"/> that describe each endpoint.</para>
    ///   <para>If successful, returns HTTP status code 200 (OK) along with the list of links.</para>
    /// </returns>
    /// <response code="200">Returns the <see cref="RootResponse"/> containing the links to registered endpoints.</response>
    /// <example>
    ///     GET /api/root
    ///     Response:
    ///     HTTP/1.1 200 OK
    ///     Content-Type: application/json
    ///     {
    ///         "links": [
    ///             {
    ///                 "href": "http://localhost:6000/api/users",
    ///                 "rel": "GetUsers",
    ///                 "method": "GET"
    ///             },
    ///             {
    ///                 "href": "http://localhost:6000/api/products",
    ///                 "rel": "GetProducts",
    ///                 "method": "GET"
    ///             }
    ///         ]
    ///     }
    /// </example>
    [HttpGet(Name = "GetRoot")]
    public async Task<ActionResult<RootResponse>> GetRoot()
    {
        HttpContext? context = HttpContextAccessor?.HttpContext;
        string remoteIpAddress = string.Empty;

        if (context is not null &&
            context.Request.Headers.TryGetValue(Constants.ForwardedFor, out StringValues ipAddress))
        {
            remoteIpAddress = ipAddress.ToString();
        }

        // Get all registered endpoints
        IEnumerable<RouteEndpoint>? endpoints = context?.RequestServices.GetRequiredService<IEndpointRouteBuilder>().DataSources
                                                        .SelectMany(ds => ds.Endpoints)
                                                        .OfType<RouteEndpoint>();

        List<Links> links = new List<Links>();

        foreach (RouteEndpoint endpoint in endpoints!)
        {
            string? routePattern = endpoint.RoutePattern.RawText;
            string httpMethods = endpoint.Metadata
                                         .OfType<HttpMethodMetadata>()
                                         .SelectMany(m => m.HttpMethods)
                                         .FirstOrDefault() ?? "GET"; // Default to GET if no methods are specified
            string routeName = endpoint.Metadata
                                       .OfType<RouteNameMetadata>()
                                       .FirstOrDefault()?.RouteName ?? "Unnamed";
            string url = $"{context?.Request.Scheme}://{remoteIpAddress}{context?.Request.PathBase}{routePattern}";

            links.Add(new Links(href: url, rel: routeName, method: httpMethods));
        }

        return Ok(await Task.FromResult(new RootResponse(links.AsReadOnly())));
    }
}