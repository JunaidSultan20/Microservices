namespace Identity.Infrastructure.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> modelBuilder)
    {
        modelBuilder.ToTable("Role");
    }
}