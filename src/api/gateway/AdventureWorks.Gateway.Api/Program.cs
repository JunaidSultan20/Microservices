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

//builder.Services.AddCorsPolicy("GatewayCorsPolicy");

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//                      policy =>
//                      {
//                          policy.WithOrigins("https://localhost:6004").AllowAnyHeader().AllowAnyMethod();
//                          policy.WithOrigins("http://localhost:6003").AllowAnyHeader().AllowAnyMethod();
//                      });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                      cors => cors.AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1-identity", new OpenApiInfo { Title = "AdventureWorks.Identity.Api", Version = "v1" });
    options.SwaggerDoc(name: "v1-sales", new OpenApiInfo { Title = "AdventureWorks.Sales.Api", Version = "v1" });
});

builder.Services.AddOcelot();

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
    });
}

//app.UseCors("GatewayCorsPolicy");

//app.UseCors("AllowSpecificOrigin");

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

//app.UseConsul(configuration);

app.Run();