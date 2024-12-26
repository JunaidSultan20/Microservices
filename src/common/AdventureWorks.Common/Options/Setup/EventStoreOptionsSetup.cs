namespace AdventureWorks.Common.Options.Setup;

/// <summary>
/// Sets up the configuration for <see cref="EventStoreOptions"/> using the provided <see cref="IConfiguration"/>.
/// </summary>
/// <param name="configuration">The application configuration used to bind settings.</param>
public class EventStoreOptionsSetup(IConfiguration configuration) : IConfigureOptions<EventStoreOptions>
{
    private const string SectionName = "EventStoreDbConfig";

    /// <summary>
    /// Configures the specified <see cref="EventStoreOptions"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="EventStoreOptions"/> instance to configure.</param>
    public void Configure(EventStoreOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}