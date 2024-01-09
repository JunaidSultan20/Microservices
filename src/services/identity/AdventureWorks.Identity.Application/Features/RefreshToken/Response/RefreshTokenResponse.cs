namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class RefreshTokenResponse : ApiResult
{
    public RefreshTokenResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }

    public RefreshTokenResponse(string message) : this(HttpStatusCode.OK, message)
    {
    }
}