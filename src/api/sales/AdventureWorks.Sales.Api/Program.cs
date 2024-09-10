using AdventureWorks.Common;
using AdventureWorks.Common.Filters;
using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Messaging;
using AdventureWorks.Middlewares.Logging;
using AdventureWorks.Sales.Api.BackgroundServices;
using AdventureWorks.Sales.Customers;
using AdventureWorks.Sales.Customers.Features.GetCustomers.Response;
using AdventureWorks.Sales.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using AdventureWorks.Events;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//Options configuration begin
builder.Services.ConfigureOptions<SeqOptionsSetup>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.ConfigureOptions<RequestLogOptionsSetup>();

builder.Services.AddSingleton<IOptionsMonitor<RequestLogOptions>, OptionsMonitor<RequestLogOptions>>();

//builder.Services.AddSingleton<RequestLogOptions>();
//Options configuration end

builder.Host.UseCustomSeriLog(builder.Services);

builder.Services.AddResponseCaching();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetValue<string>("RequestLogDbConfig:ServerUri")));

builder.Services.MessagingLayer();

builder.Services.CustomersLayer(configuration);

builder.Services.AddSalesInfrastructureLayer(configuration);

builder.Services.CommonLayer();

builder.Services.AddEventStoreLayer();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddControllerExtension("120SecondsCacheProfile");

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<GetCustomersResponse>();

builder.Services.AddApiVersioning(1, 0);

//builder.Services.AddVersionedApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'VVV";
//    options.SubstituteApiVersionInUrl = true;
//});

builder.Services.SuppressDefaultModelState();

builder.Services.AddJwtAuthentication();

builder.Services.ConfigureForwardedHeaders();

//builder.Services.AddMassTransit(x =>
//{
//    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("animal", false));

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(configuration.GetValue<string>("RabbitMqOptions:Hostname"),
//                 port: configuration.GetValue<ushort>("RabbitMqOptions:Port"), virtualHost: "/", host =>
//        {
//            host.Username(builder.Configuration.GetValue("RabbitMqOptions:Username", "guest"));
//            host.Password(builder.Configuration.GetValue("RabbitMqOptions:Password", "guest"));
//        });
//        cfg.ConfigureEndpoints(context);
//    });
//});

builder.Services.AddHostedService<RabbitMqBackgroundService>();

builder.Services.AddCustomMediaTypes(new[] { "application/vnd.api.hateoas+json" });

builder.Services.AddScoped<RequestHeaderFilter>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//builder.Services.AddConsul(configuration);

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();

app.MapHeartbeatEndpoint();

//app.UseCors("AllowSpecificOrigin");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseForwardedHeaders();

app.MapControllers();

app.UseResponseCaching();

app.Run();