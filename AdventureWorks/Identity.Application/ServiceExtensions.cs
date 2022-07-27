using System.Reflection;
using Identity.Domain.Entities;
using Identity.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Identity.Application;

public static class ServiceExtensions
{
    public static void AddIdentityApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<UserManager<User>>();
        services.AddScoped<RoleManager<Role>>();
    }
}