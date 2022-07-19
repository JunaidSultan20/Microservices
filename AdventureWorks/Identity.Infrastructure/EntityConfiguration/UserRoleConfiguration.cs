namespace Identity.Infrastructure.EntityConfiguration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> modelBuilder)
    {
        modelBuilder.ToTable("UserRole");
    }
}