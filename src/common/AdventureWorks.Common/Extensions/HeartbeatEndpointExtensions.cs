namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for mapping heartbeat endpoints in a <see cref="WebApplication"/>.
/// </summary>
public static class HeartbeatEndpointExtensions
{
    /// <summary>
    /// Maps a heartbeat endpoint to the <see cref="WebApplication"/> that responds with an OK status.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to which the heartbeat endpoint will be mapped.</param>
    /// <returns>The updated <see cref="WebApplication"/> instance with the mapped heartbeat endpoint.</returns>
    public static WebApplication MapHeartbeatEndpoint(this WebApplication app)
    {
        app.MapGet(pattern: "/api/heartbeat",
                   handler: () => Results.Ok(new ApiResult(statusCode: HttpStatusCode.OK, message: "Heartbeat check performed")));

        return app;
    }
}