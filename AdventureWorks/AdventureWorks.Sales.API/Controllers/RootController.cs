using System.Net;
using AdventureWorks.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Sales.API.Controllers;

[Route("api")]
[ApiController]
public class RootController : ControllerBase
{
    [HttpGet(Name = "GetRoot")]
    public async Task<ActionResult<BaseResponse<IList<Links>>>> GetRoot()
    {
        return Ok(await Task.Run(() =>
        {
            IList<Links> links = new List<Links>
            {
                new Links(Url.Link("GetRoot", new { }), "self", "GET"),
                new Links(Url.Link("GetCustomerList", new { pageNumber = 1, pageSize = 10, ver = 1.0 }), "customer", "GET"),
                new Links(Url.Link("GetCustomerById", new { id = 1 }), "customer", "GET")
            };
            return new BaseResponse<IList<Links>>(HttpStatusCode.OK, null, links);
        }));
    }
}