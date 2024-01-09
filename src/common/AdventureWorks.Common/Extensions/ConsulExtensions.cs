namespace AdventureWorks.Common.Extensions;

public static class ConsulExtensions
{
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

    public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
    {
        var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
        var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        if (app.Properties["server.Features"] is not FeatureCollection features)
        {
            return app;
        }

        var servicePort = int.Parse(configuration.GetValue<string>("ConsulConfig:ServicePort"));
        //var serviceIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        var serviceIp = "localhost";
        var serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
        var serviceId = serviceName + "-" + Guid.NewGuid();

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