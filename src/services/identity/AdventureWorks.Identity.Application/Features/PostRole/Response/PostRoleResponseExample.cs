namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

public class PostRoleResponseExample : IExamplesProvider<PostRoleResponse>
{
    public PostRoleResponse GetExamples()
    {
        return new PostRoleResponse(HttpStatusCode.Created, Messages.RoleCreated);
    }
}