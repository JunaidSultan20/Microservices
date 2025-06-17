namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a user's lockout status being changed.
/// </summary>
/// <remarks>
/// This class indicates whether the user's lockout status has been changed.
/// </remarks>
/// <param name="lockoutChanged">Whether the user's lockout status has been changed.</param>
public class LockoutEnabledChanged(bool lockoutChanged)
{
    /// <summary>
    /// Gets or sets whether the user's lockout status has been changed.
    /// </summary>
    public bool LockoutChanged { get; private set; } = lockoutChanged;
}