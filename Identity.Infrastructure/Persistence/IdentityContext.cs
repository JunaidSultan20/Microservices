using Identity.Domain.Entities;
using Identity.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

public class IdentityContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public IdentityContext()
    {
        
    }

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
        
    }

    public virtual DbSet<Role> Role { get; set; }
    public virtual DbSet<RoleClaim> RoleClaim { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<UserClaim> UserClaim { get; set; }
    public virtual DbSet<UserLogin> UserLogin { get; set; }
    public virtual DbSet<UserRole> UserRole { get; set; }
    public virtual DbSet<UserToken> UserToken { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=IdentityDatabase;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
    }
}