namespace AdventureWorks.Common.Options.Setup;

/// <summary>
/// Sets up the configuration for <see cref="RabbitMqOptions"/> using the provided <see cref="IConfiguration"/>.
/// </summary>
/// <param name="configuration">The application configuration used to bind settings.</param>
public class RabbitMqOptionsSetup(IConfiguration configuration) : IConfigureOptions<RabbitMqOptions>
{
    private const string SectionName = "RabbitMqOptions";

    /// <summary>
    /// Configures the specified <see cref="RabbitMqOptions"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="RabbitMqOptions"/> instance to configure.</param>
    public void Configure(RabbitMqOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        //Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}