namespace AdventureWorks.Identity.Application.DomainEvents.Roles;

public class RoleNameChanged(string oldName, string newName)
{
    public string OldName { get; set; } = oldName;

    public string NewName { get; set; } = newName;

    public string OldNormalizedName { get; set; } = oldName.ToUpper();

    public string NewNormalizedName { get; set; } = newName.ToUpper();
}