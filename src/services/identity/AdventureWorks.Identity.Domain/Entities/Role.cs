namespace AdventureWorks.Identity.Domain.Entities;

public class Role : IdentityRole<int>
{
    public Role()
    {
    }

    public Role(string name, string normalizedName) => (Name, NormalizedName) = (name, normalizedName);
}