namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a password being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new password hashes.
/// </remarks>
/// <param name="oldPasswordHash">The hash of the original password.</param>
/// <param name="newPasswordHash">The hash of the new password.</param>
public class PasswordChanged(string oldPasswordHash, string newPasswordHash)
{
    /// <summary>
    /// Gets or sets the hash of the original password.
    /// </summary>
    public string OldPasswordHash { get; set; } = oldPasswordHash;

    /// <summary>
    /// Gets or sets the hash of the new password.
    /// </summary>
    public string NewPasswordHash { get; private set; } = newPasswordHash;
}