using System.Reflection;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ServiceExtensions
{
    public static void AddIdentityApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<UserManager<User>>();
        services.AddScoped<RoleManager<Role>>();
    }
}