namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class TwoFactorEnabledChanged(bool isConfirmed)
{
    public bool IsConfirmed { get; set; } = isConfirmed;
}