using System.Text.Json;

namespace AdventureWorks.Sales.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Produces(contentType: Constants.ContentTypeJson, Constants.ContentTypeJsonHateoas,
                                                                           Constants.ContentTypeXmlHateoas,
                                                                           Constants.ContentTypeXml,
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
    /// <param name="id">Customer id for which customer is to be fetched.</param>
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

    /// <summary>
    /// Create new customer
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns base response of **CustomerDto** type</returns>
    [HttpPost(Name = "CreateCustomer", Order = 3)]
    [ProducesResponseType(typeof(PostCustomerResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(PostCustomerResponse), (int)HttpStatusCode.BadRequest)]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<PostCustomerResponse>> CreateCustomer([FromBody] CreateCustomerDto customer,
                                                                         CancellationToken cancellationToken = default)
    {
        PostCustomerResponse response = await Mediator.Send(new PostCustomerRequest(customer), cancellationToken);
        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        return CreatedAtAction("GetCustomerById", new { id = response.Result?.CustomerId }, response);
    }

    ///// <summary>
    ///// Updates customer by id.
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="customer"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns>Returns the updated customer record.</returns>
    ///// <exception cref="Exception"></exception>
    ///// <remarks>
    ///// Sample request (this request updates the **customer**)
    ///// PUT /gateway/customer/1
    /////     [
    /////         {
    /////             "personId": null,
    /////             "storeId": 934,
    /////             "territoryId": 1,
    /////             "accountNumber": "AW00000001",
    /////             "modifiedDate": "2014-09-12T11:15:07.263"
    /////         }
    /////     ]
    ///// </remarks>
    //[HttpPut("{id:int:min(1):required}", Name = "UpdateCustomerById", Order = 4)]
    //[ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NoContent)]
    //[ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NotFound)]
    //[ProducesErrorResponseType(typeof(BaseResponse<CustomerDto>))]
    //public async Task<ActionResult<BaseResponse<CustomerDto>>> UpdateCustomerById([FromRoute] int id,
    //    [FromBody] UpdateCustomerDto customer, CancellationToken cancellationToken = default)
    //{
    //    if (customer is null)
    //        throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(customer)));

    //    BaseResponse<CustomerDto> response = await Mediator.Send(new UpdateCustomerCommand(id, customer), cancellationToken);

    //    return response.StatusCode switch
    //    {
    //        HttpStatusCode.NotFound => NotFound(response),
    //        HttpStatusCode.NoContent => StatusCode((int)HttpStatusCode.NoContent, response),
    //        _ => BadRequest(response)
    //    };
    //}

    ///// <summary>
    ///// Patches customer by id.
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="customer"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns>Returns the updated customer record.</returns>
    ///// <exception cref="Exception"></exception>
    ///// <remarks>
    ///// Sample request (this request updates the **customer**)
    ///// PATCH /gateway/customer/1
    /////     [
    /////         {
    /////             "op": "replace",
    /////             "path": "/storeId",
    /////             "value": 456
    /////         }
    /////     ]
    ///// </remarks>
    //[HttpPatch("{id:int:min(1):required}", Name = "PatchCustomerById", Order = 5)]
    //[ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NoContent)]
    //[ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NotFound)]
    //[ProducesErrorResponseType(typeof(BaseResponse<CustomerDto>))]
    //public async Task<ActionResult<BaseResponse<CustomerDto>>> PatchCustomerById([FromRoute] int id,
    //    [FromBody] JsonPatchDocument customer,
    //    CancellationToken cancellationToken = default)
    //{
    //    BaseResponse<CustomerDto> response =
    //        await Mediator.Send(new PatchCustomerByIdCommand(id, customer), cancellationToken);

    //    return response.StatusCode switch
    //    {
    //        HttpStatusCode.NotFound => NotFound(response),
    //        HttpStatusCode.NoContent => StatusCode((int)HttpStatusCode.NoContent, response),
    //        _ => BadRequest(response)
    //    };
    //}

    /// <summary>
    /// Delete customer by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns base response of object type.</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{id:int:min(1):required}", Name = "DeleteCustomerById", Order = 6)]
    [ProducesResponseType(typeof(DeleteCustomerResponse), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(NotFoundCustomerResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestCustomerResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<DeleteCustomerResponse>> DeleteCustomerById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        DeleteCustomerResponse response = await Mediator.Send(new DeleteCustomerRequest(id), cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NoContent => StatusCode((int)HttpStatusCode.NoContent, response),
            HttpStatusCode.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
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