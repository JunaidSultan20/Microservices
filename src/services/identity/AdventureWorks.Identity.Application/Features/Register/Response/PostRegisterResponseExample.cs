namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class PostRegisterResponseExample : IExamplesProvider<PostRegisterResponse>
{
    public PostRegisterResponse GetExamples()
    {
        return new PostRegisterResponse(HttpStatusCode.Created, 
                                        message: Messages.UserCreatedSuccessfully, 
                                        result: new UserDto(Username: "jane.doe", Email: "jane.doe@adventureworks.com"));
    }
}