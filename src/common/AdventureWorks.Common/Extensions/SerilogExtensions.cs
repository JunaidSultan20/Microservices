namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring Serilog in an application.
/// </summary>
public static class SerilogExtensions
{
    /// <summary>
    /// Configures and uses a custom Serilog logger for the specified <see cref="IHostBuilder"/> instance.
    /// </summary>
    /// <param name="hostBuilder">The <see cref="IHostBuilder"/> instance to configure with Serilog.</param>
    /// <param name="service">The <see cref="IServiceCollection"/> used to retrieve configuration options.</param>
    public static void UseCustomSeriLog(this IHostBuilder hostBuilder, IServiceCollection service)
    {
        IOptions<SeqOptions> seqOptions = service.BuildServiceProvider().GetRequiredService<IOptions<SeqOptions>>();

        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Storage.IRelationalCommandBuilderFactory", LogEventLevel.Warning)
                    .Enrich.WithProperty("ApplicationContext", Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty)
                    //.Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Seq(seqOptions.Value.Server, LogEventLevel.Information, apiKey: seqOptions.Value.ApiKey)
                    .WriteTo.Http(seqOptions.Value.Server, null, restrictedToMinimumLevel: LogEventLevel.Information)
                    .CreateLogger();

        hostBuilder.UseSerilog();
    }
}