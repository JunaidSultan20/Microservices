namespace AdventureWorks.Common;

public static class ServiceExtension
{
    public static void CommonLayer(this IServiceCollection services)
    {
        services.AddSingleton<IUrlService, UrlService>();
    }
}