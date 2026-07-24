using AdventureWorks.Common.Extensions;
using AdventureWorks.Identity.Application;
using AdventureWorks.Identity.Infrastructure;
using System.Reflection;
using AdventureWorks.Events;
using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;
using AdventureWorks.Middlewares.Logging;
using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Common.Options;
using AdventureWorks.Middlewares.RequestId;
using Microsoft.Extensions.Options;
using Consul;
using MongoDB.Driver;

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

//builder.Services.ConfigureOptions<EventStoreOptionsSetup>();

//builder.Services.AddSingleton<EventStoreOptions>();

//builder.Services.AddSingleton<JwtOptions>();

builder.Services.ConfigureOptions<RequestLogOptionsSetup>();

builder.Services.AddSingleton<IOptionsMonitor<RequestLogOptions>, OptionsMonitor<RequestLogOptions>>();

builder.Services.AddIdentityApplicationLayer();

builder.Services.AddIdentityInfrastructureLayer(builder.Configuration);

builder.Services.AddEventStoreLayer();

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetValue<string>("RequestLogDbConfig:ServerUri")));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddJwtAuthentication();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddConsul(configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    string filePath = Path.Combine(AppContext.BaseDirectory, "AdventureWorks.Identity.Api.xml");
    swagger.IncludeXmlComments(filePath);
});

builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<PostLoginResponse>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<PostLoginRequest>();

var app = builder.Build();

app.UseMiddleware<RequestIdMiddleware>();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>(false);

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseConsul(app.Configuration);

app.MapControllers();

app.Run();