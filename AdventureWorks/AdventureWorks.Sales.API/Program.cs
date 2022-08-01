var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", cors => cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true));
});

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
    options.Filters.Add<ModelValidationFilter>();
    options.ReturnHttpNotAcceptable = true;
    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
})
.AddFluentValidation(v =>
{
    v.RegisterValidatorsFromAssemblyContaining<UpdateCustomerDtoValidator>();
    v.AutomaticValidationEnabled = true;
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(resolver => resolver.First());
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    options.IncludeXmlComments(xmlCommentPath);
});
builder.Services.AddHealthChecks();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-Version"));
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF32.GetBytes(configuration.GetValue<string>("JwtConfig:Secret"))),
        ValidateAudience = true,
        ValidAudiences = new List<string> { "sales.api" },
        ValidIssuer = configuration.GetValue<string>("JwtConfig:Issuer"),
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        RequireExpirationTime = true,
        RequireAudience = true
    };
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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AdventureWorks.Sales API V1");
        //options.DefaultModelExpandDepth(2);
        //options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        //options.EnableDeepLinking();
        //options.DisplayOperationId();
    });
}

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AdventureWorks.Sales API V1");
    //options.DefaultModelExpandDepth(2);
    //options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
    //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    //options.EnableDeepLinking();
    //options.DisplayOperationId();
});

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

//app.MapGet("/", async context =>
//{
//    await context.Response.WriteAsync(serviceName);
//});

app.Run();