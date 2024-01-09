namespace AdventureWorks.Identity.Infrastructure.EntityConfiguration;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> modBuilder)
    {
        modBuilder.ToTable(name: "UserLogin");
    }
}