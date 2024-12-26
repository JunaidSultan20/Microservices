namespace AdventureWorks.Identity.Application.DomainEvents.Roles;

/// <summary>
/// Represents the event of a role being created.
/// </summary>
/// <remarks>
/// This class provides information about the name of the newly created role, including its normalized version.
/// </remarks>
/// <param name="name">The name of the created role.</param>
/// <param name="normalizedName">The normalized version of the role name (in uppercase).</param>
public class RoleCreated(string name, string normalizedName)
{
    /// <summary>
    /// Gets or sets the name of the created role.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the normalized version of the role name (in uppercase).
    /// </summary>
    public string NormalizedName { get; set; } = normalizedName;
}