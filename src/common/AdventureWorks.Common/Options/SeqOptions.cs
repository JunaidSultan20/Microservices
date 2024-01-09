namespace AdventureWorks.Common.Options;

public class SeqOptions
{
    [Required]
    public string Server { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}