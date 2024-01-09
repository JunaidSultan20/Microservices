namespace AdventureWorks.Sales.Api.Controllers;

[Produces(contentType: Constants.ContentTypeJson, Constants.ContentTypeJsonHateoas,
                                                                           Constants.ContentTypeXmlHateoas,
                                                                           Constants.ContentTypeXml,
                                                                           Constants.ContentTypeTextPlain,
                                                                           Constants.ContentTypeTextJson)]
public class StoreController : BaseController<StoreController>
{
    public StoreController(IMediator mediator,
                           IHttpContextAccessor httpContextAccessor,
                           ILogger<StoreController> logger) :
                           base(mediator: mediator,
                                httpContextAccessor: httpContextAccessor,
                                logger: logger)
    {
    }

    //[HttpGet(template: "{id:int:min(1):required}", Name = "GetStoreById", Order = 1)]
    //[ProducesResponseType(type: typeof(StoreDtoExample), statusCode: (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<BaseResponse<StoreDto>>> GetStoreById([FromRoute] int id,
    //                                                                     [BindRequired, FromHeader(Name = "Accept")] string mediaType,
    //                                                                     CancellationToken cancellationToken = default)
    //{
    //    if (!HelperMethods.CheckIfMediaTypeIsValid(mediaType: mediaType,
    //                                               parsedMediaType: out MediaTypeHeaderValue? parsedMediaType,
    //                                               responseValue: out BaseResponse<StoreDto>? responseValue))
    //        return BadRequest(responseValue);

    //    BaseResponse<StoreDto> response = await Mediator.Send(request: new GetStoreByIdQuery(id), cancellationToken: cancellationToken);

    //    if (response.StatusCode == HttpStatusCode.NotFound)
    //        return NotFound(response);

    //    if (response.Result is not null && parsedMediaType is not null && parsedMediaType.ToString().Contains("vnd.api.hateoas"))
    //        response.Links = CreateStoreLinks(id: response.Result.CustomerId, fields: null);

    //    return Ok(response);
    //}

    //[HttpGet(template: "{id:int:min(1):required}/customer", Name = "GetCustomerListByStoreId", Order = 2)]
    //public async Task<ActionResult<BaseResponse<StoreWithCustomersDto>>> GetCustomerListByStoreId([FromRoute] int id,
    //                                                                                               CancellationToken cancellationToken = default)
    //{
    //    BaseResponse<StoreWithCustomersDto> response = await Mediator.Send(request: new GetCustomersByStoreIdQuery(id),
    //                                                                       cancellationToken: cancellationToken);

    //    if (response.StatusCode == HttpStatusCode.NotFound)
    //        return NotFound(response);

    //    return Ok(response);
    //}

    //[HttpGet(template: "{salesPersonId:int:min(1):required}/salesPersonId", Name = "GetStoreBySalesPersonId", Order = 3)]
    //public async Task<ActionResult<BaseResponse<IReadOnlyList<StoreDto>>>> GetStoreBySalesPersonId([FromRoute] int salesPersonId,
    //                                                                                                CancellationToken cancellationToken = default)
    //{
    //    BaseResponse<IReadOnlyList<StoreDto>> response = await Mediator.Send(request: new GetStoreBySalesPersonQuery(salesPersonId),
    //                                                                         cancellationToken: cancellationToken);

    //    if (response.StatusCode == HttpStatusCode.NotFound)
    //        return NotFound(response);

    //    return Ok(response);
    //}

    #region Links Helper Region

    private IReadOnlyList<Links> CreateStoreLinks(int id, string? fields)
    {
        var context = HttpContextAccessor.HttpContext;
        Links link;
        List<Links> links = new List<Links>();
        if (!string.IsNullOrWhiteSpace(fields))
        {
            link = new Links(href: Url.RouteUrl(routeName: "GetStoreById",
                                                values: new { id, fields }),
                                                rel: Constants.SelfRel,
                                                method: Constants.GetMethod);
            link.Href = link.Href?.Replace(oldValue: Constants.ApiValue,
                                           newValue: $"{context?.Request.Scheme}://{RemoteIpAddress}/gateway");
            links.Add(link);
        }
        else
        {
            link = new Links(href: Url.RouteUrl(routeName: "GetStoreById",
                                                values: new { id }),
                                                rel: Constants.SelfRel,
                                                method: Constants.GetMethod);
            link.Href = link.Href?.Replace(oldValue: Constants.ApiValue,
                                           newValue: $"{context?.Request.Scheme}://{RemoteIpAddress}/gateway");
            links.Add(link);
        }

        link = new Links(href: Url.RouteUrl(routeName: "DeleteStoreById",
                                            values: new { id }),
                                            rel: "delete_store",
                                            method: Constants.DeleteMethod);
        link.Href = link.Href?.Replace(oldValue: Constants.ApiValue,
                                       newValue: $"{context?.Request.Scheme}://{RemoteIpAddress}/gateway");
        links.Add(link);

        link = new Links(href: Url.RouteUrl(routeName: "UpdateStoreById",
                                            values: new { id }),
                                            rel: "update_store",
                                            method: Constants.PutMethod);
        link.Href = link.Href?.Replace(oldValue: Constants.ApiValue,
                                       newValue: $"{context?.Request.Scheme}://{RemoteIpAddress}/gateway");
        links.Add(link);

        return links.AsReadOnly();
    }
    #endregion
}