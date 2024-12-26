namespace AdventureWorks.Common;

public static class ServiceExtension
{
    /// <summary>
    /// Registers common services in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <remarks>
    /// This method adds the <see cref="IUrlService"/> service as a singleton in the dependency injection container.
    /// The <see cref="UrlService"/> implementation is registered for the <see cref="IUrlService"/> interface.
    /// </remarks>
    public static void CommonLayer(this IServiceCollection services)
    {
        services.AddSingleton<IUrlService, UrlService>();
    }
}