using AdventureWorks.Common.Constants;
using AdventureWorks.Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RootController(IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    /// <summary>
    /// Root method to return URL for all endpoints in the API.
    /// </summary>
    /// <returns>A list of available API endpoints as a <see cref="RootResponse"/>.</returns>
    [HttpGet(Name = "GetRoot")]
    public async Task<ActionResult<RootResponse>> GetRoot()
    {
        var context = httpContextAccessor.HttpContext;
        string remoteIpAddress = string.Empty;

        if (context is not null &&
            context.Request.Headers.TryGetValue(Constants.ForwardedFor, out var ipAddress))
        {
            remoteIpAddress = ipAddress.ToString();
        }

        // Get all registered endpoints
        var endpoints = context?.RequestServices.GetRequiredService<IEndpointRouteBuilder>().DataSources
                                .SelectMany(ds => ds.Endpoints)
                                .OfType<RouteEndpoint>();

        var links = new List<Links>();

        foreach (var endpoint in endpoints)
        {
            var routePattern = endpoint.RoutePattern.RawText;
            var httpMethods = endpoint.Metadata
                                      .OfType<HttpMethodMetadata>()
                                      .SelectMany(m => m.HttpMethods)
                                      .FirstOrDefault() ?? "GET"; // Default to GET if no methods are specified

            var routeName = endpoint.Metadata
                                    .OfType<RouteNameMetadata>()
                                    .FirstOrDefault()?.RouteName ?? "Unnamed";

            var url = $"{context.Request.Scheme}://{remoteIpAddress}{context.Request.PathBase}{routePattern}";

            links.Add(new Links(
                                href: url,
                                rel: routeName,
                                method: httpMethods
                               ));
        }

        return Ok(await Task.FromResult(new RootResponse(links.AsReadOnly())));
    }

}