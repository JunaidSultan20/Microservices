namespace AdventureWorks.Sales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeartbeatController : ControllerBase
{
    public HeartbeatController()
    {
    }

    [HttpGet]
    public IActionResult CheckHeartbeat()
    {
        return Ok(new ApiResult(HttpStatusCode.OK, "Heartbeat check performed"));
    }
}