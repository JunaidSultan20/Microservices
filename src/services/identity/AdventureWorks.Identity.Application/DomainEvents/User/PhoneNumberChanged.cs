namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class PhoneNumberChanged(string oldPhoneNumber, string newPhoneNumber)
{
    public string OldPhoneNumber { get; set; } = oldPhoneNumber;

    public string NewPhoneNumber { get; set; } = newPhoneNumber;
}