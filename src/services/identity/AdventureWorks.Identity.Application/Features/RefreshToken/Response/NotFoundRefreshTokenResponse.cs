namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class NotFoundRefreshTokenResponse(string message) : RefreshTokenResponse(HttpStatusCode.NotFound, Messages.UserNotFound);