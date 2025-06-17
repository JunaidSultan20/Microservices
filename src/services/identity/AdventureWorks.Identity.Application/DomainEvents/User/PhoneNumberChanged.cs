namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a phone number being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new phone numbers.
/// </remarks>
/// <param name="oldPhoneNumber">The original phone number.</param>
/// <param name="newPhoneNumber">The new phone number.</param>
public class PhoneNumberChanged(string oldPhoneNumber, string newPhoneNumber)
{
    /// <summary>
    /// Gets or sets the original phone number.
    /// </summary>
    public string OldPhoneNumber { get; set; } = oldPhoneNumber;

    /// <summary>
    /// Gets or sets the new phone number.
    /// </summary>
    public string NewPhoneNumber { get; private set; } = newPhoneNumber;
}