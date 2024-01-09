namespace AdventureWorks.Identity.Application.Features.Login.Response;

public class PostUnauthorizedAttemptResponse : PostLoginResponse
{
    public PostUnauthorizedAttemptResponse() : base(HttpStatusCode.Unauthorized, Messages.UnauthorizedAttempt)
    {
    }
}