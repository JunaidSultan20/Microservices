namespace AdventureWorks.Identity.Application.DomainEvents.Roles;

public class RoleCreated(string name, string normalizedName)
{
    public string Name { get; set; } = name;

    public string NormalizedName { get; set; } = normalizedName;
}