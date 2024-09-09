namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

public class BadRequestPostRoleResponseExample : IExamplesProvider<BadRequestPostRoleResponse>
{
    public BadRequestPostRoleResponse GetExamples() => new BadRequestPostRoleResponse(Messages.UnableToCreateRole);
}