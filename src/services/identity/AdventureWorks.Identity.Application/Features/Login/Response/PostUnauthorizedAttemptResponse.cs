namespace AdventureWorks.Identity.Application.Features.Login.Response;

public class PostUnauthorizedAttemptResponse() : PostLoginResponse(HttpStatusCode.Unauthorized, Messages.UnauthorizedAttempt);