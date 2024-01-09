namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class NotFoundRefreshTokenResponse : RefreshTokenResponse
{
    public NotFoundRefreshTokenResponse(string message) : base(HttpStatusCode.NotFound, Messages.UserNotFound)
    {
    }
}