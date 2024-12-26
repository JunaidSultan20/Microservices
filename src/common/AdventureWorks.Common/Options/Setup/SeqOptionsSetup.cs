namespace AdventureWorks.Common.Options.Setup;

/// <summary>
/// Sets up the configuration for <see cref="SeqOptions"/> using the provided <see cref="IConfiguration"/>.
/// </summary>
/// <param name="configuration">The application configuration used to bind settings.</param>
public class SeqOptionsSetup(IConfiguration configuration) : IConfigureOptions<SeqOptions>
{
    private const string SectionName = "SeqOptions";

    /// <summary>
    /// Configures the specified <see cref="SeqOptions"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="SeqOptions"/> instance to configure.</param>
    public void Configure(SeqOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}