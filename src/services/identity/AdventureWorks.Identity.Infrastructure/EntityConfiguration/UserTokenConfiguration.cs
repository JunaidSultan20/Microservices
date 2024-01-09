namespace AdventureWorks.Identity.Infrastructure.EntityConfiguration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> modelBuilder)
    {
        modelBuilder.ToTable(name: "UserToken");
    }
}