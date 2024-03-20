namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class PasswordChanged(string oldPasswordHash, string newPasswordHash)
{
    public string OldPasswordHash { get; set; } = oldPasswordHash;

    public string NewPasswordHash { get; set; } = newPasswordHash;
}