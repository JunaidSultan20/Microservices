namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a user's lockout end time being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new lockout end times.
/// </remarks>
/// <param name="oldLockoutEnd">The original lockout end time, or null if the user wasn't locked out.</param>
/// <param name="newLockoutEnd">The new lockout end time, or null if the user's lockout has been removed.</param>
public class LockoutEndChanged(DateTime? oldLockoutEnd, DateTime? newLockoutEnd)
{
    /// <summary>
    /// Gets or sets the original lockout end time, or null if the user wasn't locked out.
    /// </summary>
    public DateTime? OldLockoutEnd { get; set; } = oldLockoutEnd;

    /// <summary>
    /// Gets or sets the new lockout end time, or null if the user's lockout has been removed.
    /// </summary>
    public DateTime? NewLockoutEnd { get; set; } = newLockoutEnd;
}