namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of an email address being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new email addresses.
/// </remarks>
/// <param name="oldEmail">The original email address.</param>
/// <param name="newEmail">The new email address.</param>
public class EmailChanged(string oldEmail, string newEmail)
{
    /// <summary>
    /// Gets or sets the original email address.
    /// </summary>
    public string OldEmail { get; set; } = oldEmail;

    /// <summary>
    /// Gets or sets the new email address.
    /// </summary>
    public string NewEmail { get; set; } = newEmail;
}