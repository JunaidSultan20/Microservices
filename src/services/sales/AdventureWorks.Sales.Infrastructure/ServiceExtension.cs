namespace AdventureWorks.Sales.Infrastructure;

public static class ServiceExtension
{
    public static void AddSalesInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdventureWorksSalesContext>(options =>
                                                              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                          optionAction => optionAction.MigrationsAssembly(typeof(AdventureWorksSalesContext).Assembly.FullName))
                                                                     .UseLazyLoadingProxies(), ServiceLifetime.Scoped);

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        services.AddHealthChecks().AddDbContextCheck<AdventureWorksSalesContext>();
    }
}