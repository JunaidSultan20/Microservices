namespace AdventureWorks.Identity.Application.Features.Login.Response;

public class PostLoginResponse : ApiResult
{
    public PostLoginResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message)
    {
    }
}