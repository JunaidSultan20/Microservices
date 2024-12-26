namespace AdventureWorks.Common.Options.Setup;

/// <summary>
/// Sets up the configuration for <see cref="RequestLogOptions"/> using the provided <see cref="IConfiguration"/>.
/// </summary>
/// <param name="configuration">The application configuration used to bind settings.</param>
public class RequestLogOptionsSetup(IConfiguration configuration) : IConfigureOptions<RequestLogOptions>
{
    private const string SectionName = "RequestLogDbConfig";

    /// <summary>
    /// Configures the specified <see cref="RequestLogOptions"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="RequestLogOptions"/> instance to configure.</param>
    public void Configure(RequestLogOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}