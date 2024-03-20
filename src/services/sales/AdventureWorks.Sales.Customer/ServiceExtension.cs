using AdventureWorks.Sales.Customers.DomainEvents;

namespace AdventureWorks.Sales.Customers;

public static class ServiceExtension
{
    public static void CustomersLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnectionString");
        });

        services.AddScoped<CustomerAggregate>();

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}