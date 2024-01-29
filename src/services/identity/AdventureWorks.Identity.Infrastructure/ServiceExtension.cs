namespace AdventureWorks.Identity.Infrastructure;

public static class ServiceExtension
{
    public static void AddIdentityInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
                                                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                        optionAction => optionAction.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName))
                                                          .UseLazyLoadingProxies());

        services.AddIdentity<User, Role>(options =>
                 {
                     options.User.RequireUniqueEmail = true;
                     options.User.AllowedUserNameCharacters =
                         "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_@+/.";
                     options.Password.RequireDigit = true;
                     options.Password.RequireLowercase = true;
                     options.Password.RequireNonAlphanumeric = true;
                     options.Password.RequireUppercase = true;
                     options.Password.RequiredLength = 10;
                     options.Password.RequiredUniqueChars = 1;
                     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
                     options.Lockout.MaxFailedAccessAttempts = 5;
                     options.Lockout.AllowedForNewUsers = true;
                     options.SignIn.RequireConfirmedAccount = true;
                     options.SignIn.RequireConfirmedEmail = true;
                     options.SignIn.RequireConfirmedPhoneNumber = true;
                 }).AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DataProtectorTokenProvider<User>>(Constants.LoginProviderName);

        services.Configure<PasswordHasherOptions>(options =>
        {
            options.IterationCount = 12000;
        });
    }
}