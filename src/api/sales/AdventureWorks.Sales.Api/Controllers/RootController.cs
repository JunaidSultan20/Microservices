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
                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetRoot")}", 
                          rel: Constants.SelfRel, 
                          method: Constants.GetMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetCustomerList", new { pageNumber = 1, pageSize = 10 })}",
                          rel: Constants.CustomerRel,
                          method: Constants.GetMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetCustomerById", new { id = 1 })}",
                          rel: Constants.CustomerRel,
                          method: Constants.GetMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetCustomerByIdRange", new { minId = 1, maxId = 10 })}",
                          rel: Constants.CustomerRel,
                          method: Constants.GetMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("UpdateCustomerById", new { id = 1 })}",
                          rel: Constants.CustomerRel,
                          method: Constants.PutMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("DeleteCustomerById", new { id = 1 })}",
                          rel: Constants.CustomerRel,
                          method: Constants.DeleteMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("CreateCustomer")}",
                          rel: "create_customer",
                          method: Constants.PostMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetStoreById", new { id = 934 })}",
                          rel: Constants.StoreRel,
                          method: Constants.GetMethod),

                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetCustomerListByStoreId", new { id = 934 })}",
                          rel: Constants.StoreRel,
                          method: Constants.GetMethod),
                
                new Links(href: $"{context?.Request.Scheme}://{remoteIpAddress}{Url.RouteUrl("GetStoreBySalesPersonId", new { salesPersonId = 280 })}",
                          rel: Constants.StoreRel,
                          method: Constants.GetMethod)
            };

            return new RootResponse(links.AsReadOnly());
        }));
    }
}