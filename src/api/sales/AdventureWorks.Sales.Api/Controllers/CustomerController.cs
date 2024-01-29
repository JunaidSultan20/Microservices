using AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;

namespace AdventureWorks.Sales.Api.Controllers;

[Produces(contentType: Constants.ContentTypeJson, Constants.ContentTypeJsonHateoas,
          Constants.ContentTypeTextPlain,
          Constants.ContentTypeTextJson)]
public class CustomerController : BaseController<CustomerController>
{
    public CustomerController(IMediator mediator, IHttpContextAccessor httpContextAccessor,
                              ILogger<CustomerController> logger) :
        base(mediator: mediator,
             httpContextAccessor: httpContextAccessor,
             logger: logger)
    {
    }

    /// <summary>
    /// Returns the customers list with data shaping and caching options.
    /// </summary>
    /// <param name="paginationParameters"></param>
    /// <param name="mediaType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns list of customers with pagination and optional shaped data with links.</returns>
    /// <remarks>Sample Request (this request fetches the the list of **customers**)
    ///     GET /gateway/customer?pageNumber=1&#38;pageSize=10
    /// </remarks>
    [HttpGet(Name = "GetCustomers", Order = 1)]
    [ProducesResponseType(typeof(GetCustomersResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundGetCustomersResponse), (int)HttpStatusCode.NotFound)]
    [RequiresParameter(Name = "paginationParameters", Required = true, Source = OpenApiParameterLocation.Query, Type = typeof(PaginationParameters))]
    [MapToApiVersion("1.0")]
    //[ProducesErrorResponseType(typeof(PaginationResponse<IEnumerable<CustomerWithLinksDto>>))]
    //[ResponseCache(CacheProfileName = "120SecondsCacheProfile")]
    public async Task<ActionResult<GetCustomersResponse>> GetCustomers([FromQuery] PaginationParameters paginationParameters,
                                                                       [BindRequired, FromHeader(Name = "Accept")] string mediaType,
                                                                       CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("GetCustomers action executed");

        GetCustomersResponse response = await Mediator.Send(request: new GetCustomersRequest(paginationParameters),
                                                            cancellationToken: cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(value: response);

        Response.Headers.Append(Constants.XPaginationKey, JsonSerializer.Serialize(paginationParameters));

        if (mediaType.Contains("vnd.api.hateoas"))
            response.Result?
                    .ToList()
                    .ForEach(item => item.Links = CreateCustomerLinks(id: item.CustomerId, fields: paginationParameters.Fields));

        GetCustomersShapedResponse shapedResponse = new GetCustomersShapedResponse(response.Result?.ShapeData(paginationParameters.Fields),
                                                                                   response.PaginationData);

        return Ok(value: shapedResponse);
    }

    /// <summary>
    /// Returns the customer by id provided through route.
    /// </summary>
    /// <param name="id" example="1">Customer id for which customer is to be fetched.</param>
    /// <param name="mediaType">For getting the results via vendor specific media types.</param>
    /// <param name="fields">Used for data shaping the result fields.</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>Sample Request (this request fetches customer based on the **id** provided
    ///     GET /api/customer/1
    /// </remarks>
    /// <returns>An ActionResult of type BaseResponse</returns>
    /// <resposne code="200">Returns the matched customer</resposne>
    /// <response code="404">If no user exists against the id provided</response>
    [HttpGet(template: "{id:int:min(1):required}", Name = "GetCustomerById", Order = 2)]
    [ProducesResponseType(typeof(GetCustomerByIdResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundCustomerByIdResponse), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetCustomerByIdResponse>> GetCustomerById([FromRoute] int id,
                                                                             [FromHeader(Name = "Accept")] string mediaType,
                                                                             [FromQuery] string? fields = null,
                                                                             CancellationToken cancellationToken = default)
    {
        GetCustomerByIdResponse response = await Mediator.Send(new GetCustomerByIdRequest(id), cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(value: response);

        if (response.Result is not null && mediaType.Contains(Constants.VndApiHateoas))
            response.Result.Links = CreateCustomerLinks(id: response.Result.CustomerId, fields: null);

        if (!string.IsNullOrEmpty(fields))
        {
            GetCustomerByIdShapedResponse shapedResponse = new GetCustomerByIdShapedResponse(response.Message, response.Result.ShapeData(fields));
            return Ok(shapedResponse);
        }

        return Ok(response);
    }

    #region Links Helper Region

    private IReadOnlyList<Links> CreateCustomerLinks(int id, string? fields)
    {
        var context = HttpContextAccessor.HttpContext;
        Links link;
        List<Links> links = new List<Links>();
        if (!string.IsNullOrWhiteSpace(fields))
        {
            link = new Links(Url.RouteUrl("GetCustomerById", new { id, fields }), "self", "GET");
            link.Href = link.Href?.Replace("/api", $"{context?.Request.Scheme}://{RemoteIpAddress}/api");
            links.Add(link);
        }
        else
        {
            link = new Links(Url.RouteUrl("GetCustomerById", new { id }), "self", "GET");
            link.Href = link.Href?.Replace("/api", $"{context?.Request.Scheme}://{RemoteIpAddress}/api");
            links.Add(link);
        }

        link = new Links(Url.RouteUrl("DeleteCustomerById", new { id }), "delete_customer", "DELETE");
        link.Href = link.Href?.Replace("/api", $"{context?.Request.Scheme}://{RemoteIpAddress}/api");
        links.Add(link);

        link = new Links(Url.RouteUrl("UpdateCustomerById", new { id }), "update_customer", "PUT");
        link.Href = link.Href?.Replace("/api", $"{context?.Request.Scheme}://{RemoteIpAddress}/api");
        links.Add(link);

        return links.AsReadOnly();
    }
    #endregion
}