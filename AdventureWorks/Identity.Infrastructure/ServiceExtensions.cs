using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class ServiceExtensions
{
    public static void AddIdentityInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    optionAction => optionAction.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName))
                .UseLazyLoadingProxies());
        services.AddScoped<IAccountService, AccountService>();
    }
}