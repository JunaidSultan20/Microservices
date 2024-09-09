namespace AdventureWorks.Identity.Application.Features.PostRole.Request;

public class PostRoleRequestExample : IExamplesProvider<PostRoleRequest>
{
    public PostRoleRequest GetExamples()
    {
        return new PostRoleRequest(new CreateRoleDto(name: "Guest"));
    }
}