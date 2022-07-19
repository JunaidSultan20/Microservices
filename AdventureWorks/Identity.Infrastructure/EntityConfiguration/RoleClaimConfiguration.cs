namespace Identity.Infrastructure.EntityConfiguration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> modelBuilder)
    {
        modelBuilder.ToTable("RoleClaim");
    }
}