using AdventureWorks.Common.Extensions;
using AdventureWorks.Common.Options.Setup;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Configuration.AddJsonFile(path: $"ocelot.{builder.Environment.EnvironmentName}.json",
                                  optional: false,
                                  reloadOnChange: true);

builder.Services.AddOcelot();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint(url: "https://localhost:6004/swagger/v1/swagger.json", name: "AdventureWorks.Identity.Api.v1");
        s.SwaggerEndpoint(url: "https://localhost:6006/swagger/v1/swagger.json", name: "AdventureWorks.Sales.Api.v1");
        s.SwaggerEndpoint(url: "https://localhost:6020/swagger/v1/swagger.json", name: "AdventureWorks.Jobs.Api.v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

//app.UseConsul(configuration);

app.Run();