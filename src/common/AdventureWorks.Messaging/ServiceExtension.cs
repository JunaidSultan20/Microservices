using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Contracts.Messaging;
using AdventureWorks.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

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