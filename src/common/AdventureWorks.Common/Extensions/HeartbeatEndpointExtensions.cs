namespace AdventureWorks.Common.Extensions;

public static class HeartbeatEndpointExtensions
{
    public static WebApplication MapHeartbeatEndpoint(this WebApplication app)
    {
        app.MapGet(pattern: "/api/heartbeat",
                   handler: () => Results.Ok(new ApiResult(statusCode: HttpStatusCode.OK,
                                                           message: "Heartbeat check performed")));

        return app;
    }
}