using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Messaging.Services;
using Microsoft.Extensions.Options;

namespace AdventureWorks.Messaging;

public static class ServiceExtension
{
    public static void MessagingLayer(this IServiceCollection services)
    {
        services.ConfigureOptions<RabbitMqOptionsSetup>();

        services.AddSingleton<IOptionsMonitor<RabbitMqOptions>, OptionsMonitor<RabbitMqOptions>>();

        services.AddSingleton<IConnectionFactory>(sp =>
        {
            var options = sp.GetRequiredService<IOptionsMonitor<RabbitMqOptions>>().CurrentValue;
            return new ConnectionFactory
            {
                HostName = options.Hostname,
                Port = options.Port,
                UserName = options.Username,
                Password = options.Password
            };
        });

        services.AddTransient<IMessageProducer, MessageProducer>();

        services.AddTransient<IMessageReceiver, MessageReceiver>();
    }
}