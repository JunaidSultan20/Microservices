namespace AdventureWorks.Common.Options.Setup;

public class RequestLogOptionsSetup(IConfiguration configuration) : IConfigureOptions<RequestLogOptions>
{
    private const string SectionName = "RequestLogDbConfig";

    public void Configure(RequestLogOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);

        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}