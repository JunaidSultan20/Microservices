namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a phone number confirmation status being changed.
/// </summary>
/// <remarks>
/// This class indicates whether the phone number has been confirmed.
/// </remarks>
/// <param name="isConfirmed">Whether the phone number is confirmed.</param>
public class PhoneNumberConfirmedChanged(bool isConfirmed)
{
    /// <summary>
    /// Gets or sets whether the phone number is confirmed.
    /// </summary>
    public bool IsConfirmed { get; set; } = isConfirmed;
}