namespace AdventureWorks.Common.Options;

public class JwtOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Secret { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public double ExpirationMinutes { get; set; }
}