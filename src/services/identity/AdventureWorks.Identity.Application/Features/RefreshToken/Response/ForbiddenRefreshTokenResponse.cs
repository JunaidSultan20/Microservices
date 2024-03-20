namespace AdventureWorks.Identity.Application.Features.RefreshToken.Response;

public class ForbiddenRefreshTokenResponse() : RefreshTokenResponse(HttpStatusCode.Forbidden, "Authentication cookie missing");