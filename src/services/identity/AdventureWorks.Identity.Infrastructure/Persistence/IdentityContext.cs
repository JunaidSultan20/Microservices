namespace AdventureWorks.Identity.Infrastructure.Persistence;

public class IdentityContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public IdentityContext()
    {
    }

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options: options)
    {
    }

    public virtual DbSet<Role>? Role { get; set; }
    public virtual DbSet<RoleClaim>? RoleClaim { get; set; }
    public virtual DbSet<User>? User { get; set; }
    public virtual DbSet<UserClaim>? UserClaim { get; set; }
    public virtual DbSet<UserLogin>? UserLogin { get; set; }
    public virtual DbSet<UserRole>? UserRole { get; set; }
    public virtual DbSet<UserToken>? UserToken { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder: optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString: "Server=localhost,8000;Database=AdventureWorksIdentity;User ID=sa;Password=g#Gd%9?QMcGq7pLHnqXV;Trusted_Connection=False;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(builder: modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
    }
}