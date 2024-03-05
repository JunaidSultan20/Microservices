namespace AdventureWorks.Identity.Application.Features.Login.Response;

public class PostLoginResponse(HttpStatusCode statusCode, string? message) : ApiResult(statusCode, message);