namespace AdventureWorks.Common.Options;

public class RabbitMqOptions
{
    //[Required]
    public string Hostname { get; set; } = string.Empty;

    //[Required]
    public int Port { get; set; }

    //[Required]
    public string Username { get; set; } = string.Empty;

    //[Required]
    public string Password { get; set; } = string.Empty;
}