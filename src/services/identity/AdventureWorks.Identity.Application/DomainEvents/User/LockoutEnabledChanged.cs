namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class LockoutEnabledChanged(bool lockoutChanged)
{
    public bool LockoutChanged { get; set; } = lockoutChanged;
}