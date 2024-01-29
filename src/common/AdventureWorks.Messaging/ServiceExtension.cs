using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Messaging.Services;

namespace AdventureWorks.Messaging;

public static class ServiceExtension
{
    public static void MessagingLayer(this IServiceCollection services)
    {
        services.ConfigureOptions<RabbitMqOptionsSetup>();

        services.AddTransient<IMessageProducer, MessageProducer>();

        services.AddTransient<IMessageReceiver, MessageReceiver>();
    }
}