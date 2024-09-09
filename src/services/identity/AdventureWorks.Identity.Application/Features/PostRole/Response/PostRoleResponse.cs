namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

public class PostRoleResponse(HttpStatusCode statusCode, string message) : ApiResult(statusCode, message);