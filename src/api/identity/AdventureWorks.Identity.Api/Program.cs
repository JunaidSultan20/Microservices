using AdventureWorks.Common.Extensions;
using AdventureWorks.Identity.Application;
using AdventureWorks.Identity.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddIdentityApplicationLayer();

builder.Services.AddIdentityInfrastructureLayer(builder.Configuration);

builder.Services.AddMediatR(config =>
                                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddJwtAuthentication();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

//builder.Services.AddConsul(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

//app.UseConsul(configuration);

app.Run();