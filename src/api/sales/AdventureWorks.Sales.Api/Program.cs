var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

//Options configuration begin
builder.Services.ConfigureOptions<SeqOptionsSetup>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

//Options configuration end

builder.Host.UseCustomSeriLog(builder.Services);

builder.Services.AddCorsPolicy("SalesCorsPolicy");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5000").AllowAnyHeader().AllowAnyMethod();
                          policy.WithOrigins("http://localhost:5001").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddResponseCaching();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetValue<string>("RequestLogDbConfig:ServerUri")));

builder.Services.MessagingLayer();

builder.Services.CustomersLayer(configuration);

builder.Services.AddSalesInfrastructureLayer(configuration);

builder.Services.CommonLayer();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddControllerExtension("120SecondsCacheProfile");

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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

//builder.Services.AddConsul(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseStaticFiles();

//app.UseConsul(configuration);

app.UseRouting();

app.UseCors("SalesCorsPolicy");

app.UseCors("AllowSpecificOrigin");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>(ServiceName.Sales);

app.UseAuthorization();

app.UseForwardedHeaders();

app.MapControllers();

app.UseResponseCaching();

app.Run();