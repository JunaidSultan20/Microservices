using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ServiceExtensions
{
    public static void AddIdentityApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}