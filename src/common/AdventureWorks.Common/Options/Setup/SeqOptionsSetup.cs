namespace AdventureWorks.Common.Options.Setup;

public class SeqOptionsSetup(IConfiguration configuration) : IConfigureOptions<SeqOptions>
{
    private const string SectionName = "SeqOptions";

    public void Configure(SeqOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}