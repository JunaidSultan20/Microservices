namespace AdventureWorks.Common.Options;

public class EventStoreOptions
{
    [Required(AllowEmptyStrings = false)] 
    public string ServerUri { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)] 
    public string Database { get; set; } = string.Empty;

    //[Required(AllowEmptyStrings = false)] 
    public string Collection { get; set; } = string.Empty;
}