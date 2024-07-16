namespace AdventureWorks.Identity.Application.Features.Login.Response;

public class PostLoginResponseExample : IExamplesProvider<PostLoginResponse>
{
    public PostLoginResponse GetExamples()
    {
        return new PostLoginResponse(HttpStatusCode.OK, Messages.BearerTokenGenerated);
    }
}