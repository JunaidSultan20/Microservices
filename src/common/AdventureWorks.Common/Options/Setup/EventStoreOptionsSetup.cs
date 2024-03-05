namespace AdventureWorks.Common.Options.Setup;

public class EventStoreOptionsSetup : IConfigureOptions<EventStoreOptions>
{
    private const string SectionName = "EventStoreDbConfig";
    private readonly IConfiguration _configuration;

    public EventStoreOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EventStoreOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);

        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}