using AdventureWorks.Common.Extensions;
using AdventureWorks.Common.Options;
using AdventureWorks.Identity.Application;
using AdventureWorks.Identity.Infrastructure;
using System.Reflection;
using AdventureWorks.Common.Options.Setup;
using AdventureWorks.Events;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.ConfigureOptions<EventStoreOptionsSetup>();

//builder.Services.AddSingleton<EventStoreOptions>();

//builder.Services.AddSingleton<JwtOptions>();

builder.Services.AddIdentityApplicationLayer();

builder.Services.AddIdentityInfrastructureLayer(builder.Configuration);

builder.Services.AddEventStoreLayer();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddJwtAuthentication();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
