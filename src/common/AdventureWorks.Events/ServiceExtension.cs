using AdventureWorks.Common.Options;
using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Contracts.EventStreaming;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AdventureWorks.Events;

public static class ServiceExtension
{
    public static void AddEventStoreLayer(this IServiceCollection services)
    {
        services.ConfigureOptions<EventStoreOptionsSetup>();

        services.AddSingleton<IMongoClient>(_ => 
                                                new MongoClient(services
                                                               .BuildServiceProvider()
                                                               .GetRequiredService<IOptions<EventStoreOptions>>()
                                                               .Value
                                                               .ServerUri));

        services.AddScoped<IEventStore, Services.EventStore>();
    }
}