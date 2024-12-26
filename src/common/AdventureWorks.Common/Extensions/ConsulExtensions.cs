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
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var host = configuration.GetValue<string>("ConsulConfig:ConsulHost");
            consulConfig.Address = new Uri(host);
        }));

        //services.AddHostedService<ConsulHostedService>();

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

        if (app.Properties["server.Features"] is not FeatureCollection features)
        {
            return app;
        }

        int servicePort = int.Parse(configuration.GetValue<string>("ConsulConfig:ServicePort"));
        string serviceIp = "localhost"; // Optionally, this could be retrieved dynamically.
        string serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
        string serviceId = serviceName + "-" + Guid.NewGuid();

        var registration = new AgentServiceRegistration()
        {
            ID = serviceId,
            Name = serviceName,
            Address = serviceIp.ToString(),
            Port = servicePort,
            Check = new AgentCheckRegistration()
            {
                HTTP = $"http://{serviceIp}:{servicePort}/health",
                Interval = TimeSpan.FromSeconds(10)
            }
        };

        logger.LogInformation("Registering with Consul");
        consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

        lifetime.ApplicationStopping.Register(() =>
        {
            logger.LogInformation("Unregistering from Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        });

        return app;
    }
}