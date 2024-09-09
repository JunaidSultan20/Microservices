namespace AdventureWorks.Identity.Application.Features.Register.Request;

public class PostRegisterRequestExample : IExamplesProvider<PostRegisterRequest>
{
    public PostRegisterRequest GetExamples()
    {
        return new PostRegisterRequest(new RegistrationDto(Username: "jane.doe", Email: "jane.doe@adventureworks.com", Password: "JaneDoe@123", Role: "Admin"));
    }
}