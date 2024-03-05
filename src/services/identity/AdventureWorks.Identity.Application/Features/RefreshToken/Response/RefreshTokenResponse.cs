namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class RefreshTokenResponse(HttpStatusCode statusCode, string message) : ApiResult(statusCode, message)
{
    public RefreshTokenResponse(string message) : this(HttpStatusCode.OK, message)
    {
    }
}