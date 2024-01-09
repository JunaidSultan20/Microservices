namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class ForbiddenRefreshTokenResponse : RefreshTokenResponse
{
    public ForbiddenRefreshTokenResponse() : base(HttpStatusCode.Forbidden, "Authentication cookie missing")
    {
    }
}