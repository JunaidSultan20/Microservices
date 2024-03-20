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

        services.AddTransient<IMessageProducer, MessageProducer>();

        services.AddTransient<IMessageReceiver, MessageReceiver>();
    }
}