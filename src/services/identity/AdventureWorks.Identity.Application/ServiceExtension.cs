﻿using AdventureWorks.Common.Options.Setup;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AdventureWorks.Common.Extensions;
using AdventureWorks.Identity.Application.DomainEvents;
using AdventureWorks.Identity.Application.DomainEvents.Roles;

namespace AdventureWorks.Identity.Application;

public static class ServiceExtension
{
    public static void AddIdentityApplicationLayer(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();

        //services.AddSingleton<IOptionsMonitor<JwtOptions>, OptionsMonitor<JwtOptions>>();

        services.AddSingleton<JwtOptions>();

        services.AddMediatR(config => 
                            config.RegisterServicesFromAssemblies(assemblies: Assembly.GetExecutingAssembly()));

        services.AddScoped<UserManager<User>>();

        services.AddScoped<RoleManager<Role>>();

        services.AddScoped<UserAggregate>();

        services.AddScoped<RoleAggregate>();

        //services.AddScopedAggregates();
    }
}