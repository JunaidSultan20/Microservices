namespace AdventureWorks.Common.Options.Setup;

/// <summary>
/// Sets up the configuration for <see cref="JwtOptions"/> using the provided <see cref="IConfiguration"/>.
/// </summary>
/// <param name="configuration">The application configuration used to bind settings.</param>
public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "JwtOptions";

    /// <summary>
    /// Configures the specified <see cref="JwtOptions"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="JwtOptions"/> instance to configure.</param>
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}