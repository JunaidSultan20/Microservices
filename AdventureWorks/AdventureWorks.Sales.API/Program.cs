using System.Reflection;
using System.Text.Json;
using AdventureWorks.Common;
using AdventureWorks.Sales.API.Extensions;
using AdventureWorks.Sales.API.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Sales.Application;
using Sales.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

//const string serviceName = "sales.api";

// Add services to the container.

builder.Services.AddResponseCaching();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
//builder.Services.AddConsul(configuration.GetServiceConfig());
builder.Services.AddHttpContextAccessor();
builder.Services.AddSalesApplicationLayer(configuration);
builder.Services.AddSalesInfrastructureLayer(configuration);
builder.Services.AddCommonLayer();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("120SecondsCacheProfile", new CacheProfile()
    {
        Duration = 120,
        Location = ResponseCacheLocation.Any
    });
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddResponseCaching();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});//.AddJwtBearer(options =>
//{
//   options.Authority = "https://adventureworks.us.auth0.com/";
//    options.Audience = "https://localhost:7021/api/";
//});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-Version"),
        new QueryStringApiVersionReader("ver"));
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddCustomMediaTypes();
builder.Services.AddScoped<RequestHeaderFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseResponseCaching();

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

//app.MapGet("/", async context =>
//{
//    await context.Response.WriteAsync(serviceName);
//});

app.Run();
