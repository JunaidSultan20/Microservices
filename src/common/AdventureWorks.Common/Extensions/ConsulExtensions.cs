using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for integrating Consul with the application.
/// </summary>
public static class ConsulExtensions
{
    /// <summary>
    /// Adds Consul services to the <see cref="IServiceCollection"/> by configuring the <see cref="IConsulClient"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The application's configuration used to retrieve Consul settings.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added Consul services.</returns>
    public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p =>
        {
            var host = configuration.GetValue<string>("ConsulConfig:ConsulHost");
            return new ConsulClient(cfg => cfg.Address = new Uri(host));
        });

        return services;
    }

    /// <summary>
    /// Configures the application to use Consul for service registration and deregistration.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <param name="configuration">The application's configuration used to retrieve Consul settings.</param>
    /// <returns>The configured <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
    {
        IConsulClient consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        ILogger logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
        IHostApplicationLifetime lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        if (app.Properties["server.Features"] is not FeatureCollection)
        {
            logger.LogWarning("Server features not available, skipping Consul registration.");
            return app;
        }

        string? serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
        int servicePort = configuration.GetValue<int>("ConsulConfig:ServicePort");
        string? serviceAddress = configuration.GetValue<string>("ConsulConfig:ServiceAddress") ?? "host.docker.internal";
        string? serviceId = $"{serviceName}-{Guid.NewGuid()}";

        AgentServiceRegistration? registration = new AgentServiceRegistration
        {
            ID = serviceId,
            Name = serviceName,
            Address = serviceAddress,
            Port = servicePort,
            Check = new AgentServiceCheck
            {
                HTTP = $"http://{serviceAddress}:{servicePort}/health",
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromSeconds(5),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30)
            }
        };

        lifetime.ApplicationStarted.Register(async () =>
        {
            try
            {
                logger.LogInformation("Registering {ServiceName} with Consul", serviceName);
                await consulClient.Agent.ServiceDeregister(registration.ID); // cleanup in case of old instance
                await consulClient.Agent.ServiceRegister(registration);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Consul registration failed");
            }
        });

        lifetime.ApplicationStopping.Register(async () =>
        {
            try
            {
                logger.LogInformation("Unregistering {ServiceName} from Consul", serviceName);
                await consulClient.Agent.ServiceDeregister(registration.ID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Consul deregistration failed");
            }
        });

        return app;
    }
}