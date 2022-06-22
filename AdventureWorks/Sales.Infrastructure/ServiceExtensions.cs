using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Contracts.UnitOfWork;
using Sales.Infrastructure.Repositories;

namespace Sales.Infrastructure;

public static class ServiceExtensions
{
    public static void AddSalesInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdventureWorksSalesContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    optionAction => optionAction.MigrationsAssembly(typeof(AdventureWorksSalesContext).Assembly.FullName))
                .UseLazyLoadingProxies());
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}