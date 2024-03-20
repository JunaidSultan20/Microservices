namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class LockoutEndChanged(DateTime? oldLockoutEnd, DateTime? newLockoutEnd)
{
    public DateTime? OldLockoutEnd { get; set; } = oldLockoutEnd;

    public DateTime? NewLockoutEnd { get; set; } = newLockoutEnd;
}