namespace AdventureWorks.Common.Options.Setup;

public class RabbitMqOptionsSetup(IConfiguration configuration) : IConfigureOptions<RabbitMqOptions>
{
    private const string SectionName = "RabbitMqOptions";

    public void Configure(RabbitMqOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);

        //Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}