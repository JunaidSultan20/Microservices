namespace AdventureWorks.Common.Extensions;

public static class SerilogExtensions
{
    public static void UseCustomSeriLog(this IHostBuilder hostBuilder, IServiceCollection service)
    {
        var seqOptions = service.BuildServiceProvider().GetRequiredService<IOptions<SeqOptions>>();

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