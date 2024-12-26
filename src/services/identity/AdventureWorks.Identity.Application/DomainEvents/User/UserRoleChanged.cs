namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a user's role being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new role IDs.
/// </remarks>
/// <param name="oldRole">The ID of the user's original role, or null if the user didn't have a role before.</param>
/// <param name="newRole">The ID of the user's new role.</param>
public class UserRoleChanged(int? oldRole, int newRole)
{
    /// <summary>
    /// Gets the ID of the user's original role, or null if the user didn't have a role before.
    /// </summary>
    public int? OldRole { get; private set; } = oldRole;

    /// <summary>
    /// Gets the ID of the user's new role.
    /// </summary>
    public int NewRole { get; private set; } = newRole;
}