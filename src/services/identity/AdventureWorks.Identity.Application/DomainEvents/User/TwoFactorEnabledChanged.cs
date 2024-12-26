namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of two-factor authentication being enabled or disabled.
/// </summary>
/// <remarks>
/// This class indicates whether two-factor authentication has been confirmed.
/// </remarks>
/// <param name="isConfirmed">Whether two-factor authentication is confirmed.</param>
public class TwoFactorEnabledChanged(bool isConfirmed)
{
    /// <summary>
    /// Gets or sets whether two-factor authentication is confirmed.
    /// </summary>
    public bool IsConfirmed { get; set; } = isConfirmed;
}