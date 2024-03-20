using AdventureWorks.Common.Events;
using Newtonsoft.Json.Serialization;

namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Services extensions class.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Custom media type services extension.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="mediaTypes"></param>
    public static void AddCustomMediaTypes(this IServiceCollection services, string[] mediaTypes)
    {
        services.Configure<MvcOptions>(config =>
        {
            var newtonsoftJsonOutputFormatter = config.OutputFormatters
                                                      .OfType<NewtonsoftJsonOutputFormatter>()
                                                      .FirstOrDefault();

            if (newtonsoftJsonOutputFormatter is not null)
            {
                foreach (string type in mediaTypes)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add(type);
                }
            }

            var xmlOutputFormatter = config.OutputFormatters
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
    /// Jwt authentication services extension
    /// </summary>
    /// <param name="services"></param>
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>();

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
                //ValidAudiences = configuration.GetValue<string>("JwtOptions:Audience")!.Split(',').Select(x => x.Trim()).ToList(),
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
    /// Caching extension for the controllers
    /// </summary>
    /// <param name="service"></param>
    /// <param name="profileName"></param>
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
    /// Extension method to add cors policy
    /// </summary>
    /// <param name="services"></param>
    /// <param name="policyName"></param>
    public static void AddCorsPolicy(this IServiceCollection services, string policyName)
    {
        //services.AddCors(options =>
        //{
        //    options.AddPolicy(policyName, 
        //        cors => 
        //        {
        //            cors.AllowAnyOrigin()
        //                .AllowAnyHeader()
        //                .AllowAnyMethod();
        //        });
        //});

        services.AddCors(options => options.AddPolicy(policyName, builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }));
    }

    /// <summary>
    /// Extension method for adding the api versioning
    /// </summary>
    /// <param name="services"></param>
    /// <param name="majorVersion"></param>
    /// <param name="minorVersion"></param>
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
    /// Extension method for suppressing the default model validation filter
    /// </summary>
    /// <param name="services"></param>
    public static void SuppressDefaultModelState(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    /// <summary>
    /// Extension method for configuring the forwarded header options
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureForwardedHeaders(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedForHeaderName = Constants.Constants.ForwardedFor;
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
    }

    /// <summary>
    /// Registers each event specific aggregate class that is inherited from Aggregate class
    /// </summary>
    /// <param name="services"></param>
    public static void AddScopedAggregates(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var aggregateType = typeof(Aggregate);

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(aggregateType) && !type.IsAbstract)
                services.AddScoped(type);
        }
    }
}