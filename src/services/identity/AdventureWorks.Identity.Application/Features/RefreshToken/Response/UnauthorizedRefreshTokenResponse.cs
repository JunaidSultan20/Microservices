namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class UnauthorizedRefreshTokenResponse(string message) : RefreshTokenResponse(HttpStatusCode.Unauthorized, message)
{
}