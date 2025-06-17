namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of an email confirmation status being changed.
/// </summary>
/// <remarks>
/// This class indicates whether the email address has been confirmed.
/// </remarks>
/// <param name="isConfirmed">Whether the email address is confirmed.</param>
public class EmailConfirmedChanged(bool isConfirmed)
{
    /// <summary>
    /// Gets or sets whether the email address is confirmed.
    /// </summary>
    public bool IsConfirmed { get; private set; } = isConfirmed;
}