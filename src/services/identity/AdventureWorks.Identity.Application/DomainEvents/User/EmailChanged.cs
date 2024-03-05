namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class EmailChanged(string oldEmail, string newEmail)
{
    public string OldEmail { get; set; } = oldEmail;

    public string NewEmail { get; set; } = newEmail;
}