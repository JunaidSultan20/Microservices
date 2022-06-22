using System.Dynamic;
using System.Net;
using System.Text.Json;
using AdventureWorks.Common.Helpers;
using AdventureWorks.Common.Parameters;
using AdventureWorks.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs;
using Sales.Application.Features.Customers.Queries;

namespace AdventureWorks.Sales.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns the customers list with data shaping and caching options.
    /// </summary>
    /// <param name="paginationParameters"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<IEnumerable<ExpandoObject>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationResponse<IEnumerable<ExpandoObject>>>> GetAllCustomers([FromQuery] PaginationParameters paginationParameters)
    {
        PaginationResponse<IEnumerable<CustomerDto>> response =
            await _mediator.Send(new GetAllCustomersQuery(paginationParameters));

        if (response.StatusCode == HttpStatusCode.NotFound)
            return NotFound(response);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(response.PaginationData));
        PaginationResponse<IEnumerable<ExpandoObject>> shapedResponse =
            new PaginationResponse<IEnumerable<ExpandoObject>>(response.StatusCode, response.Message,
                response?.Result?.ShapeData(paginationParameters.Fields), response?.PaginationData);

        return Ok(shapedResponse);
    }

    /// <summary>
    /// Returns the customer by id provided through route.
    /// </summary>
    /// <param name="id">Customer Id.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BaseResponse<CustomerDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BaseResponse<CustomerDto>>> GetById([FromRoute] int id)
    {
        BaseResponse<CustomerDto> response = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(response);
    }

    /// <summary>
    /// Get customer by customer id range.
    /// </summary>
    /// <param name="minId"></param>
    /// <param name="maxId"></param>
    /// <returns></returns>
    [HttpGet("customerRange")]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<CustomerDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BaseResponse<IEnumerable<CustomerDto>>>> GetCustomerByIdRange(
        [FromQuery] int minId, [FromQuery] int maxId)
    {
        BaseResponse<IEnumerable<CustomerDto>> response =
            await _mediator.Send(new GetCustomersByIdRangeQuery(minId, maxId));
        return Ok(response);
    }
}