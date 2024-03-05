namespace AdventureWorks.Common.Options.Setup;

public class RequestLogOptionsSetup : IConfigureOptions<RequestLogOptions>
{
    private const string SectionName = "RequestLogDbConfig";
    private readonly IConfiguration _configuration;

    public RequestLogOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RequestLogOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);

        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}