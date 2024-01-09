namespace AdventureWorks.Sales.Infrastructure;

public static class ServiceExtensions
{
    public static void AddSalesInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdventureWorksSalesContext>(options =>
                                                              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                          optionAction => optionAction.MigrationsAssembly(typeof(AdventureWorksSalesContext).Assembly.FullName))
                                                                     .UseLazyLoadingProxies(), ServiceLifetime.Transient);

        services.AddTransient<AdventureWorksSalesContext>();

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

        services.AddHealthChecks().AddDbContextCheck<AdventureWorksSalesContext>();
    }
}