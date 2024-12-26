using AdventureWorks.Common.Events;
using Newtonsoft.Json.Serialization;

namespace AdventureWorks.Common.Extensions;

/// <summary>
/// A collection of extension methods for configuring various services in the application, 
/// such as JWT authentication, custom media types, CORS policies, API versioning, and more.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds support for custom media types for JSON and XML formatters.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="mediaTypes">An array of custom media types to support.</param>
    public static void AddCustomMediaTypes(this IServiceCollection services, string[] mediaTypes)
    {
        services.Configure<MvcOptions>(config =>
        {
            NewtonsoftJsonOutputFormatter? outputFormatter= config.OutputFormatters
                                                                  .OfType<NewtonsoftJsonOutputFormatter>()
                                                                  .FirstOrDefault();

            if (outputFormatter is not null)
            {
                foreach (string type in mediaTypes)
                {
                    outputFormatter.SupportedMediaTypes.Add(type);
                }
            }

            XmlDataContractSerializerOutputFormatter? xmlOutputFormatter = config.OutputFormatters
                                                                                 .OfType<XmlDataContractSerializerOutputFormatter>()
                                                                                 .FirstOrDefault();

            if (xmlOutputFormatter is not null)
            {
                foreach (string type in mediaTypes)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add(type);
                }
            }
        });
    }

    /// <summary>
    /// Configures JWT authentication for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        IOptions<JwtOptions> jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Authority = "https://localhost:6002";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(jwtOptions.Value.Secret ?? string.Empty)),
                ValidateAudience = false,
                ValidAudiences = new List<string>
                {
                    "https://sales.api"
                },
                ValidIssuer = jwtOptions.Value.Issuer,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                RequireAudience = true
            };
        });
    }

    /// <summary>
    /// Adds caching profiles and configuration for the controllers.
    /// </summary>
    /// <param name="service">The <see cref="IServiceCollection"/> to configure.</param>
    /// <param name="profileName">The name of the caching profile.</param>
    public static void AddControllerExtension(this IServiceCollection service, string profileName)
    {
        service.AddControllers(options =>
        {
            options.CacheProfiles.Add(profileName, new CacheProfile
            {
                Duration = 120,
                Location = ResponseCacheLocation.Any
            });
            options.Filters.Add<ModelValidationFilter>();
            options.ReturnHttpNotAcceptable = true;
            options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            options.RespectBrowserAcceptHeader = true;
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }).AddXmlDataContractSerializerFormatters();

        service.Configure<MvcOptions>(options =>
        {
            options.OutputFormatters.RemoveType<XmlDataContractSerializerOutputFormatter>();
        });
    }

    /// <summary>
    /// Configures CORS policy for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="policyName">The name of the CORS policy.</param>
    public static void AddCorsPolicy(this IServiceCollection services, string policyName)
    {
        services.AddCors(options => options.AddPolicy(policyName, builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }));
    }

    /// <summary>
    /// Configures API versioning for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="majorVersion">The major version number for the API.</param>
    /// <param name="minorVersion">The minor version number for the API.</param>
    public static void AddApiVersioning(this IServiceCollection services, int majorVersion, int minorVersion)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-Version"));
        });
    }

    /// <summary>
    /// Suppresses the default model state validation filter.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    public static void SuppressDefaultModelState(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    /// <summary>
    /// Configures forwarded headers options for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    public static void ConfigureForwardedHeaders(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedForHeaderName = Constants.Constants.ForwardedFor;
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
    }

    /// <summary>
    /// Registers event-specific aggregate classes derived from the <see cref="Aggregate"/> class.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    public static void AddScopedAggregates(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type aggregateType = typeof(Aggregate);

        foreach (Type type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(aggregateType) && !type.IsAbstract)
                services.AddScoped(type);
        }
    }
}