namespace AdventureWorks.Identity.Application.DomainEvents.Roles;

/// <summary>
/// Represents the domain event of a role name being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new role names, including their normalized versions.
/// </remarks>
/// <param name="oldName">The original name of the role.</param>
/// <param name="newName">The new name of the role.</param>
public class RoleNameChanged(string oldName, string newName)
{
    /// <summary>
    /// Gets or sets the original name of the role.
    /// </summary>
    public string OldName { get; set; } = oldName;

    /// <summary>
    /// Gets or sets the new name of the role.
    /// </summary>
    public string NewName { get; set; } = newName;

    /// <summary>
    /// Gets or sets the normalized version of the original name (in uppercase).
    /// </summary>
    public string OldNormalizedName { get; set; } = oldName.ToUpper();

    /// <summary>
    /// Gets or sets the normalized version of the new name (in uppercase).
    /// </summary>
    public string NewNormalizedName { get; set; } = newName.ToUpper();
}