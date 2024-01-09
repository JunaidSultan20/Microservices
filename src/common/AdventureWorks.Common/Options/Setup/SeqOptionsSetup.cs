namespace AdventureWorks.Common.Options.Setup;

public class SeqOptionsSetup : IConfigureOptions<SeqOptions>
{
    private const string SectionName = "SeqOptions";
    private readonly IConfiguration _configuration;

    public SeqOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(SeqOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);

        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}