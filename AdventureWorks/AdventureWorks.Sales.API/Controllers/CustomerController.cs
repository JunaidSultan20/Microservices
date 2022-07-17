using System.Dynamic;
using System.Net;
using System.Text.Json;
using AdventureWorks.Common.Helpers;
using AdventureWorks.Common.Parameters;
using AdventureWorks.Common.Response;
using AdventureWorks.Common.Services.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Sales.Application.DTOs;
using Sales.Application.Features.Customers.Commands;
using Sales.Application.Features.Customers.Queries;

namespace AdventureWorks.Sales.API.Controllers;

[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUrlService _urlService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _environment;

    public CustomerController(IMediator mediator, IUrlService urlService, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
    {
        _mediator = mediator ?? throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(mediator)));
        _urlService = urlService ?? throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(urlService)));
        _httpContextAccessor = httpContextAccessor ?? throw new Exception("Argument Null Exception",
            new ArgumentNullException(nameof(httpContextAccessor)));
        _environment = environment ?? throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(environment)));
    }

    /// <summary>
    /// Returns the customers list with data shaping and caching options.
    /// </summary>
    /// <param name="paginationParameters"></param>
    /// <param name="mediaType"></param>
    /// <returns>Returns list of customers with pagination and optional shaped data & links.</returns>
    [HttpGet(Name = "GetCustomerList", Order = 0)]
    [ProducesResponseType(typeof(PaginationResponse<IEnumerable<ExpandoObject>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(PaginationResponse<IEnumerable<CustomerWithLinksDto>>), (int)HttpStatusCode.NotFound)]
    [ProducesErrorResponseType(typeof(PaginationResponse<IEnumerable<CustomerWithLinksDto>>))]
    public async Task<ActionResult<PaginationResponse<IEnumerable<ExpandoObject>>>> GetAllCustomers(
        [FromQuery] PaginationParameters paginationParameters, [FromHeader(Name = "Accept")] string mediaType)
    {
        if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            return BadRequest(
                new BaseResponse<PaginationResponse<IEnumerable<ExpandoObject>>>(HttpStatusCode.BadRequest,
                    "Bad Request", null).Errors = new List<string> { "Invalid media type provided" });

        PaginationResponse<IEnumerable<CustomerWithLinksDto>> response =
            await _mediator.Send(new GetAllCustomersQuery(paginationParameters));

        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(response);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(response.PaginationData));

        if (parsedMediaType.MediaType.Equals("application/vnd.api.hateoas+json"))
            response?.Result?.ToList().ForEach(item => item.Links = CreateCustomerLinks(item.Id, paginationParameters.Fields));

        PaginationResponse<IEnumerable<ExpandoObject>> shapedResponse =
            new PaginationResponse<IEnumerable<ExpandoObject>>(response.StatusCode, response.Message,
                response?.Result?.ShapeData(paginationParameters.Fields), response?.PaginationData);

        //var shapedData = response?.Result?.ShapeData(paginationParameters.Fields).Select(customer =>
        //{
        //    var customerDictionary = customer as IDictionary<string, object>;
        //    var customerLinks = CreateCustomerLinks((int)customerDictionary["Id"], paginationParameters.Fields);
        //    customerDictionary.Add("links", customerLinks);
        //    return customerDictionary;
        //});

        return Ok(shapedResponse);
    }

    /// <summary>
    /// Returns the customer by id provided through route.
    /// </summary>
    /// <param name="id">Customer Id.</param>
    /// <returns></returns>
    [HttpGet("{id:int:min(1)}", Name = "GetCustomerById", Order = 1)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BaseResponse<CustomerDto>>> GetById([FromRoute] int id)
    {
        BaseResponse<CustomerDto> response = await _mediator.Send(new GetCustomerByIdQuery(id));
        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(response);
        response.Links = CreateCustomerLinks(response.Result.Id, null);
        return Ok(response);
    }

    /// <summary>
    /// Get customer by customer id range.
    /// </summary>
    /// <param name="minId"></param>
    /// <param name="maxId"></param>
    /// <returns></returns>
    [HttpGet("customerRange", Name = "GetCustomerByIdRange", Order = 2)]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<CustomerDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BaseResponse<IEnumerable<CustomerDto>>>> GetCustomerByIdRange(
        [FromQuery] int minId, [FromQuery] int maxId)
    {
        BaseResponse<IEnumerable<CustomerDto>> response =
            await _mediator.Send(new GetCustomersByIdRangeQuery(minId, maxId));
        return Ok(response);
    }

    /// <summary>
    /// Updates customer by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customer"></param>
    /// <returns>Returns the updated customer record.</returns>
    /// <exception cref="Exception"></exception>
    [HttpPut("{id:int:min(1):required}", Name = "UpdateCustomerById", Order = 4)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.NotFound)]
    [ProducesErrorResponseType(typeof(BaseResponse<CustomerDto>))]
    public async Task<ActionResult<BaseResponse<CustomerDto>>> UpdateCustomerById([FromRoute] int id,
        [FromBody] CustomerDto customer)
    {
        if (customer is null)
            throw new Exception("Argument Null Exception", new ArgumentNullException(nameof(customer)));
        BaseResponse<CustomerDto> response = await _mediator.Send(new UpdateCustomerByIdCommand(id, customer));
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
        if (id < 1)
            throw new Exception("Id can not be less than 1", new ArgumentOutOfRangeException(nameof(id)));
        BaseResponse<object> response = await _mediator.Send(new DeleteCustomerByIdQuery(id));
        if (response.StatusCode == HttpStatusCode.NotFound)
            return BadRequest(response);
        return StatusCode((int)HttpStatusCode.NoContent, response);
    }

    #region Links Helper Region

    //private IReadOnlyList<Links> CreateCustomerLinks(int id, string? fields)
    //{
    //    List<Links> links = new List<Links>();
    //    if (!string.IsNullOrWhiteSpace(fields))
    //    {
    //        links.Add(new Links(Url.Link("GetCustomerById", new { id, fields }), "self", "GET"));
    //    }
    //    else
    //    {
    //        links.Add(new Links(Url.Link("GetCustomerById", new { id }), "self", "GET"));
    //    }

    //    links.Add(new Links(Url.Link("DeleteCustomerById", new { id }), "delete_customer", "DELETE"));

    //    links.Add(new Links(Url.Link("UpdateCustomerById", new { id }), "update_customer", "PUT"));

    //    string remoteIpAddress = string.Empty;
    //    if (_httpContextAccessor.HttpContext is not null &&
    //        _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
    //        remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];

    //    if (_environment.EnvironmentName.Equals("Local"))
    //    {
    //        links.ForEach(item =>
    //        {
    //            item.Href = item?.Href?.Replace("localhost:4100", remoteIpAddress);
    //            item.Href = item?.Href?.Replace("/api", "/gateway");
    //        });
    //    }
    //    else
    //    {
    //        links.ForEach(item =>
    //        {
    //            item.Href = item?.Href?.Replace("sales.api", remoteIpAddress);
    //            item.Href = item?.Href?.Replace("/api", "/gateway");
    //        });
    //    }
    //    return links;
    //}

    private IReadOnlyList<Links> CreateCustomerLinks(int id, string fields)
    {
        Links link;
        string remoteIpAddress = string.Empty;
        if (_httpContextAccessor.HttpContext is not null &&
            _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];

        List<Links> links = new List<Links>();
        if (!string.IsNullOrWhiteSpace(fields))
        {
            link = new Links(Url.RouteUrl("GetCustomerById", new { id, fields }), "self", "GET");
            link.Href = link.Href?.Replace("/api", $"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}/gateway");
            links.Add(link);
        }
        else
        {
            link = new Links(Url.RouteUrl("GetCustomerById", new { id }), "self", "GET");
            link.Href = link.Href?.Replace("/api", $"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}/gateway");
            links.Add(link);
        }

        link = new Links(Url.RouteUrl("DeleteCustomerById", new { id }), "delete_customer", "DELETE");
        link.Href = link.Href?.Replace("/api", $"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}/gateway");
        links.Add(link);

        link = new Links(Url.Link("UpdateCustomerById", new { id }), "update_customer", "PUT");
        link.Href = link.Href?.Replace("/api", $"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}/gateway");
        links.Add(link);

        return links;
    }
    #endregion
}