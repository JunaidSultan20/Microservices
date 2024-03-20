namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class UserRoleChanged(int? oldRole, int newRole)
{
    public int? OldRole { get; private set; } = oldRole;

    public int NewRole { get; private set; } = newRole;
}