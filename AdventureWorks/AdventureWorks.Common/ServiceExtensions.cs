using AdventureWorks.Common.Services.Contracts;
using AdventureWorks.Common.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Common;

public static class ServiceExtensions
{
    public static void AddCommonLayer(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, CacheService>();
        services.AddTransient<IUrlService, UrlService>();
    }
}