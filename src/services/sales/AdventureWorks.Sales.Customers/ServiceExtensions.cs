﻿namespace AdventureWorks.Sales.Customers;

public static class ServiceExtensions
{
    public static void CustomersLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnectionString");
        });

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}