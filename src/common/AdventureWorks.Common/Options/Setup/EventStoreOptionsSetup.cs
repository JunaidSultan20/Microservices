namespace AdventureWorks.Common.Options.Setup;

public class EventStoreOptionsSetup(IConfiguration configuration) : IConfigureOptions<EventStoreOptions>
{
    private const string SectionName = "EventStoreDbConfig";

    public void Configure(EventStoreOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}