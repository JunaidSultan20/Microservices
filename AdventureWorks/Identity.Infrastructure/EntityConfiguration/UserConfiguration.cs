namespace Identity.Infrastructure.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.ToTable("User");
    }
}