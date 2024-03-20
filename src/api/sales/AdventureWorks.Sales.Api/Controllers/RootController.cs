namespace AdventureWorks.Sales.Api.Controllers;

/// <inheritdoc />
[Route(template: "api")]
[ApiController]
public class RootController(IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    /// <summary>
    /// Root method to return url for all endpoints in the API.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetRoot")]
    public async Task<ActionResult<RootResponse>> GetRoot()
    {
        var context = httpContextAccessor.HttpContext;
        string remoteIpAddress = string.Empty;

        if (context is not null &&
            context.Request.Headers.TryGetValue(Constants.ForwardedFor, out var ipAddress))
            remoteIpAddress = ipAddress.ToString();

        return Ok(await Task.Run(function: () =>
        {
            List<Links> links = new List<Links>
            {
                new Links(href: Url.RouteUrl(routeName: "GetRoot")?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway/"),
                                            rel: Constants.SelfRel,
                                            method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "GetCustomerList",
                                             values: new { pageNumber = 1, pageSize = 10 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: Constants.CustomerRel,
                                            method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "GetCustomerById",
                                             values: new { id = 1 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: Constants.CustomerRel, method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "GetCustomerByIdRange",
                                             values: new { minId = 1, maxId = 10 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: Constants.CustomerRel,
                                            method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "UpdateCustomerById",
                                             values: new { id = 1 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: Constants.CustomerRel,
                                            method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "DeleteCustomerById",
                                             values: new { id = 1 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: Constants.CustomerRel,
                                            method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "CreateCustomer")?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: "create_customer",
                                            method: Constants.PostMethod),

                new Links(href: Url.RouteUrl(routeName: "GetStoreById",
                                             values: new { id = 934 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: Constants.StoreRel,
                                            method: Constants.GetMethod),

                new Links(href: Url.RouteUrl(routeName: "GetCustomerListByStoreId",
                                             values: new { id = 934 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: "store",
                                            method: "GET"),

                new Links(href: Url.RouteUrl(routeName: "GetStoreBySalesPersonId",
                                             values: new { salesPersonId = 280 })?
                                   .Replace(oldValue: Constants.ApiValue,
                                            newValue: $"{context?.Request.Scheme}://{remoteIpAddress}/gateway"),
                                            rel: "store",
                                            method: "GET")
            };

            return new RootResponse(links.AsReadOnly());
        }));
    }
}