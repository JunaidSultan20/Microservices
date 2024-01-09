namespace AdventureWorks.Identity.Application;

public static class ServiceExtensions
{
    public static void AddIdentityApplicationLayer(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();

        services.AddMediatR(configuration: config =>
                                config.RegisterServicesFromAssemblies(assemblies: Assembly.GetExecutingAssembly()));

        services.AddScoped<UserManager<User>>();

        services.AddScoped<RoleManager<Role>>();
    }
}