namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class UnauthorizedRefreshTokenResponse : RefreshTokenResponse
{
    public UnauthorizedRefreshTokenResponse(string message) : base(HttpStatusCode.Unauthorized, message)
    {
    }
}