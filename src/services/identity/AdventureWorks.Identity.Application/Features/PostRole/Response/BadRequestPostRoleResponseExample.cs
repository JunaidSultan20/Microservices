namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

/// <summary>
/// Provides examples for the BadRequestPostRoleResponse class.
/// </summary>
/// <remarks>
/// This class implements the IExamplesProvider interface and provides a sample BadRequestPostRoleResponse instance.
/// </remarks>
public class BadRequestPostRoleResponseExample : IExamplesProvider<BadRequestPostRoleResponse>
{
    /// <summary>
    /// Gets a sample BadRequestPostRoleResponse instance.
    /// </summary>
    /// <returns>A BadRequestPostRoleResponse instance.</returns>
    public BadRequestPostRoleResponse GetExamples() => new BadRequestPostRoleResponse(Messages.UnableToCreateRole);
}