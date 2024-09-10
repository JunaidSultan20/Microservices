using AdventureWorks.Controllers;
using AdventureWorks.Sales.Customers.Dto;
using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;
using AdventureWorks.Sales.Customers.Features.DeleteCustomer.Response;
using AdventureWorks.Sales.Customers.Features.GetCustomerById.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Request;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;
using AdventureWorks.Sales.Customers.Features.PostCustomer.Request;
using AdventureWorks.Sales.Customers.Features.PostCustomer.Response;
using Microsoft.AspNetCore.Authorization;

namespace AdventureWorks.Sales.Api.Controllers;

/// <summary>
/// Customer controller
/// </summary>
[Produces(contentType: Constants.ContentTypeJson, Constants.ContentTypeJsonHateoas,
          Constants.ContentTypeTextPlain,
          Constants.ContentTypeTextJson)]
//[Authorize]
public class CustomerController(IServiceProvider serviceProvider) : BaseController<CustomerController>(serviceProvider)
{
    /// <summary>
    /// Retrieves a list of customers based on pagination parameters.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves a paginated list of customers from the database based on the provided pagination parameters.
    /// </remarks>
    /// <param name="paginationParameters">Pagination parameters for specifying page number, page size, sorting, etc.</param>
    /// <param name="mediaType">The media type requested by the client.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>
    ///   <para>Returns a paginated list of customers.</para>
    ///   <para>If successful, returns HTTP status code 200 (OK).</para>
    ///   <para>If no customers are found based on the provided parameters, returns HTTP status code 404 (NotFound).</para>
    /// </returns>
    /// <response code="200">Returns a paginated list of customers.</response>
    /// <response code="404">No customers found based on the provided parameters.</response>
    /// <example>
    ///     GET /api/customers?page=1&amp;pageSize=10
    ///     Response:
    ///     HTTP/1.1 200 OK
    ///     Content-Type: application/json
    ///     {
    ///         "statusCode": 200,
    ///         "message": "Records found successfully",
    ///         "isSuccessful: true,
    ///         "result": [
    ///             {
    ///                 "customerId": 1,
    ///                 "personId": 10,
    ///                 "storeId": 468,
    ///                 "territoryId": 197,
    ///                 "accountNumber": "AWN256GD298",
    ///                 "modifiedDate": null,
    ///                 "links": [
    ///                     {
    ///                         "rel": "self",
    ///                         "href": "/api/customers/1"
    ///                     },
    ///                     // Additional link objects
    ///                 ]
    ///             },
    ///             // Additional customer objects
    ///         ],
    ///         "pagination": {
    ///             "totalRecords": 100,
    ///             "currentPage": 1,
    ///             "pageSize": 10,
    ///             "totalPages": 10,
    ///             "hasPrevious": false,
    ///             "hasNext": true,
    ///             "previousPageLink": null,
    ///             "nextPageLink": "/api/customers?page=2&amp;pageSize=10"
    ///         }
    ///     }
    /// </example>
    [HttpGet(Name = "GetCustomers", Order = 1)]
    [ProducesResponseType(typeof(GetCustomersResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundGetCustomersResponse), (int)HttpStatusCode.NotFound)]
    [RequiresParameter(Name = nameof(paginationParameters), Required = true, Source = OpenApiParameterLocation.Query, Type = typeof(PaginationParameters))]
    [MapToApiVersion("1.0")]
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

    /// <summary>
    /// Creates new customer
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost(Name = "PostCustomer", Order = 3)]
    public async Task<ActionResult<PostCustomerResponse>> PostCustomer([FromBody] CreateCustomerDto dto, 
                                                                       CancellationToken cancellationToken = default)
    {
        PostCustomerResponse response = await Mediator.Send(new PostCustomerRequest(dto), cancellationToken);

        return CreatedAtRoute(nameof(GetCustomerById), new { id = response.Result?.CustomerId }, response);
    }

    /// <summary>
    /// Deletes a customer by their unique identifier.
    /// </summary>
    /// <remarks>
    /// This endpoint deletes a customer record from the database based on the provided ID.
    /// </remarks>
    /// <param name="id" example="1">The unique identifier of the customer to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>
    ///   <para>Returns a status code indicating the success of the operation.</para>
    ///   <para>If successful, returns HTTP status code 204 (NoContent).</para>
    ///   <para>If the customer with the given ID does not exist, returns HTTP status code 404 (NotFound).</para>
    ///   <para>If the provided ID is not valid or other validation errors occur, returns HTTP status code 400 (BadRequest).</para>
    /// </returns>
    /// <response code="204">Customer deleted successfully.</response>
    /// <response code="404">Customer with the given ID not found.</response>
    /// <response code="400">The request is invalid.</response>
    /// <example>
    ///     DELETE /api/customers/1
    ///     Response:
    ///     HTTP/1.1 204 No Content
    /// </example>
    /// <example>
    ///     DELETE /api/customers/1000
    ///     Response:
    ///     HTTP/1.1 404 Not Found
    /// </example>
    /// <example>
    ///     DELETE /api/customers/abc
    ///     Response:
    ///     HTTP/1.1 400 Bad Request
    ///     Content-Type: application/json
    ///     {
    ///         "message": "The request is invalid.",
    ///         "errors": {
    ///             "id": [
    ///                 "The value 'abc' is not valid."
    ///             ]
    ///         }
    ///     }
    /// </example>
    [HttpDelete(template: "{id:int:min(1):required}", Name = "DeleteCustomerById", Order = 4)]
    [ProducesResponseType(typeof(DeleteCustomerResponse), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(NotFoundCustomerResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestCustomerResponse), (int)HttpStatusCode.BadRequest)]
    [RequiresParameter(Name = nameof(id), Required = true, Source = OpenApiParameterLocation.Route, Type = typeof(int))]
    public async Task<ActionResult<DeleteCustomerResponse>> DeleteCustomerById([FromRoute] int id, 
                                                                               CancellationToken cancellationToken = default)
    {
        DeleteCustomerResponse response = await Mediator.Send(new DeleteCustomerRequest(id), cancellationToken);

        return StatusCode((int)HttpStatusCode.NoContent, response);
    }

    #region Links Helper Region

    private IReadOnlyList<Links> CreateCustomerLinks(int id, string? fields)
    {
        var context = HttpContextAccessor.HttpContext;

        Links link;

        List<Links> links = new ();

        link = new Links($"{context?.Request.Scheme}://{RemoteIpAddress}{Url.RouteUrl(nameof(GetCustomerById), 
                         !string.IsNullOrWhiteSpace(fields) ? new { id, fields } : new { id })}", 
                         Constants.SelfRel, 
                         Constants.GetMethod);
        links.Add(link);

        link = new Links($"{context?.Request.Scheme}://{RemoteIpAddress}{Url.RouteUrl(nameof(DeleteCustomerById), 
                         new { id })}", 
                         "delete_customer",
                         Constants.DeleteMethod);
        links.Add(link);

        link = new Links($"{context?.Request.Scheme}://{RemoteIpAddress}{Url.RouteUrl("UpdateCustomerById", 
                         new { id })}",
                         "update_customer",
                         Constants.PutMethod);
        links.Add(link);

        return links.AsReadOnly();
    }
    #endregion
}