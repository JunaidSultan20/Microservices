namespace AdventureWorks.Common.Options.Setup;

public class RabbitMqOptionsSetup : IConfigureOptions<RabbitMqOptions>
{
    private const string SectionName = "RabbitMqOptions";
    private readonly IConfiguration _configuration;

    public RabbitMqOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RabbitMqOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);

        //Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}