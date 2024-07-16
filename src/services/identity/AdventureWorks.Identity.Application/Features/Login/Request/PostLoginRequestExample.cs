namespace AdventureWorks.Identity.Application.Features.Login.Request;

public class PostLoginRequestExample : IExamplesProvider<PostLoginRequest>
{
    public PostLoginRequest GetExamples()
    {
        return new PostLoginRequest(new AuthenticationDto("jane.doe@adventureworks.com", "janeDoe123"));
    }
}