using AdventureWorks.Common.Extensions;
using AdventureWorks.Identity.Application;
using AdventureWorks.Identity.Infrastructure;
using System.Reflection;
using AdventureWorks.Events;
using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;

var builder = WebApplication.CreateBuilder(args);

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

app.UseCors("AllowAllOrigins");

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