namespace Identity.Infrastructure.EntityConfiguration;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> modelBuilder)
    {
        modelBuilder.ToTable("UserClaim");
    }
}