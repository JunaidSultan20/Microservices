namespace AdventureWorks.Sales.API.Controllers;

[Authorize]
[ApiVersion("1.0")]
[Produces("application/json", "application/vnd.api.hateoas+json", "application/xml",
    "text/plain", "text/json")]
public class CustomerController : BaseController
{
    public CustomerController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator,
        httpContextAccessor)
    { }

    /// <summary>
    /// Returns the customers list with data shaping and caching options.
    /// </summary>
    /// <param name="paginationParameters"></param>
    /// <param name="mediaType"></param>
    /// <returns>Returns list of customers with pagination and optional shaped data with links.</returns>
    /// <remarks>Sample Request (this request fetches the the list of **customers**)
    ///     GET /gateway/customer?pageNumber=1&pageSize=10
    /// </remarks>
    [HttpGet(Name = "GetCustomerList", Order = 1)]
    [ProducesResponseType(typeof(PaginationResponse<IEnumerable<ExpandoObject>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(PaginationResponse<IEnumerable<CustomerWithLinksDto>>), (int)HttpStatusCode.NotFound)]
    [ProducesErrorResponseType(typeof(PaginationResponse<IEnumerable<CustomerWithLinksDto>>))]
    //[ResponseCache(CacheProfileName = "120SecondsCacheProfile")]
    public async Task<ActionResult<PaginationResponse<IEnumerable<ExpandoObject>>>> GetCustomerList(
        [FromQuery] PaginationParameters paginationParameters, [BindRequired, FromHeader(Name = "Accept")] string mediaType)
    {
        if (!HelperMethods.CheckIfMediaTypeIsValid(mediaType, out MediaTypeHeaderValue? parsedMediaType,
                out PaginationResponse<IEnumerable<ExpandoObject>>? responseValue))
            return StatusCode((int)HttpStatusCode.UnsupportedMediaType, responseValue);

        PaginationResponse<IEnumerable<CustomerWithLinksDto>> response =
            await Mediator.Send(new GetAllCustomersQuery(paginationParameters));

        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(response);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(response.PaginationData));

        if (parsedMediaType.MediaType.Equals("application/vnd.api.hateoas+json"))
            response.Result?.ToList().ForEach(item => item.Links = CreateCustomerLinks(item.Id, paginationParameters.Fields));
        
        PaginationResponse<IEnumerable<ExpandoObject>> shapedResponse =
            new PaginationResponse<IEnumerable<ExpandoObject>>(response.StatusCode, response.Message,
                response.Result?.ShapeData(paginationParameters.Fields), response.PaginationData);
        return Ok(shapedResponse);
    }

    /// <summary>
    /// Returns the customer by id provided through route.
    /// </summary>
    /// <param name="id">Customer id for which customer is to be fetched.</param>
    /// <param name="mediaType"></param>
    /// <remarks>Sample Request (this request fetches customer based on the **id** provided
    ///     GET /api/customer/1
    /// </remarks>
    /// <returns>An ActionResult of type BaseResponse</returns>
    /// <resposne code="200">Returns the matched customer</resposne>
    /// <response code="404">If no user exists against the id provided</response>
    [HttpGet("{id:int:min(1)}", Name = "GetCustomerById", Order = 2)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BaseResponse<CustomerDto>>> GetCustomerById([FromRoute] int id, [FromHeader(Name = "Accept")] string mediaType)
    {
        if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? parsedMediaType))
            return BadRequest(
                new BaseResponse<PaginationResponse<IEnumerable<ExpandoObject>>>(HttpStatusCode.BadRequest,
                    "Bad request",  new List<string> { "Invalid media type provided" }));

        BaseResponse<CustomerDto> response = await Mediator.Send(new GetCustomerByIdQuery(id));
        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(response);
        
        if (response.Result is not null && parsedMediaType.MediaType.Equals("application/vnd.api.hateoas+json"))
            response.Links = CreateCustomerLinks(response.Result.Id, null);

        return Ok(response);
    }

    /// <summary>
    /// Get customer by customer id range.
    /// </summary>
    /// <param name="minId"></param>
    /// <param name="maxId"></param>
    /// <param name="mediaType"></param>
    /// <returns>Returns list of customer within the range specified</returns>
    /// <remarks>
    /// GET api/customer/customerRange/?minId=1&maxId=5
    /// </remarks>
    [HttpGet("customerRange", Name = "GetCustomerByIdRange", Order = 3)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<CustomerWithLinksDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BaseResponse<IEnumerable<CustomerWithLinksDto>>>> GetCustomerByIdRange(
        [BindRequired, FromQuery] int minId, [BindRequired, FromQuery] int maxId, [FromHeader(Name = "Accept")] string mediaType)
    {
        if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? parsedMediaType))
            return BadRequest(
                new BaseResponse<PaginationResponse<IEnumerable<ExpandoObject>>>(HttpStatusCode.BadRequest,
                    "Bad request", new List<string> { "Invalid media type provided" }));

        BaseResponse<IEnumerable<CustomerWithLinksDto>> response =
            await Mediator.Send(new GetCustomersByIdRangeQuery(minId, maxId));
        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(response);

        if (parsedMediaType.MediaType.Equals("application/vnd.api.hateoas+json"))
            response.Result?.ToList().ForEach(item => item.Links = CreateCustomerLinks(item.Id, null));

        return Ok(response);
    }

    /// <summary>
    /// Updates customer by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customer"></param>
    /// <returns>Returns the updated customer record.</returns>
    /// <exception cref="Exception"></exception>
    /// <remarks>
    /// Sample request (this request updates the **customer**)
    /// PUT /gateway/customer/1
    ///     [
    ///         {
    ///             "personId": null,
    ///             "storeId": 934,
    ///             "territoryId": 1,
    ///             "accountNumber": "AW00000001",
    ///             "modifiedDate": "2014-09-12T11:15:07.263"
    ///         }
    ///     ]
    /// </remarks>
    [HttpPut("{id:int:min(1):required}", Name = "UpdateCustomerById", Order = 4)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NotFound)]
    [ProducesErrorResponseType(typeof(BaseResponse<CustomerDto>))]
    public async Task<ActionResult<BaseResponse<CustomerDto>>> UpdateCustomerById([FromRoute] int id,
        [FromBody] UpdateCustomerDto customer)
    {
        if (customer is null)
            throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(customer)));
        
        BaseResponse<CustomerDto> response = await Mediator.Send(new UpdateCustomerByIdCommand(id, customer));
        
        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.NoContent => StatusCode((int) HttpStatusCode.NoContent, response),
            _ => BadRequest(response)
        };
    }

    /// <summary>
    /// Delete customer by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns base response of object type.</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{id:int:min(1):required}", Name = "DeleteCustomerById", Order = 5)]
    [ProducesResponseType(typeof(BaseResponse<object>), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<BaseResponse<object>>> DeleteCustomerById([FromRoute] int id)
    {
        BaseResponse<object> response = await Mediator.Send(new DeleteCustomerByIdQuery(id));
        if (response.StatusCode == HttpStatusCode.NotFound)
            return BadRequest(response);
        return StatusCode((int)HttpStatusCode.NoContent, response);
    }

    /// <summary>
    /// Create new customer
    /// </summary>
    /// <param name="customer"></param>
    /// <returns>Returns base response of **CustomerDto** type</returns>
    [HttpPost(Name = "CreateCustomer", Order = 6)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BaseResponse<CustomerDto>>> CreateCustomer([FromBody] CreateCustomerDto customer)
    {
        BaseResponse<CustomerDto> response = await Mediator.Send(new CreateCustomerCommand(customer));
        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        return CreatedAtAction("GetCustomerById", new { id = response.Result?.Id }, response);
    }

    #region Links Helper Region

    private IReadOnlyList<Links> CreateCustomerLinks(int id, string? fields)
    {
        Links link;
        List<Links> links = new List<Links>();
        if (!string.IsNullOrWhiteSpace(fields))
        {
            link = new Links(Url.RouteUrl("GetCustomerById", new { id, fields }), "self", "GET");
            link.Href = link.Href?.Replace("/api", $"{HttpContextAccessor.HttpContext?.Request.Scheme}://{RemoteIpAddress}/gateway");
            links.Add(link);
        }
        else
        {
            link = new Links(Url.RouteUrl("GetCustomerById", new { id }), "self", "GET");
            link.Href = link.Href?.Replace("/api", $"{HttpContextAccessor.HttpContext?.Request.Scheme}://{RemoteIpAddress}/gateway");
            links.Add(link);
        }

        link = new Links(Url.RouteUrl("DeleteCustomerById", new { id }), "delete_customer", "DELETE");
        link.Href = link.Href?.Replace("/api", $"{HttpContextAccessor.HttpContext?.Request.Scheme}://{RemoteIpAddress}/gateway");
        links.Add(link);

        link = new Links(Url.RouteUrl("UpdateCustomerById", new { id }), "update_customer", "PUT");
        link.Href = link.Href?.Replace("/api", $"{HttpContextAccessor.HttpContext?.Request.Scheme}://{RemoteIpAddress}/gateway");
        links.Add(link);

        return links.AsReadOnly();
    }
    #endregion
}