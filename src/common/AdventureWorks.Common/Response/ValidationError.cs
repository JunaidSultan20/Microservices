namespace AdventureWorks.Common.Response;

public class ValidationError
{
    [JsonProperty(PropertyName = "field", NullValueHandling = NullValueHandling.Ignore)]
    public string? Field { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string? Message { get; }

    public ValidationError() { }

    public ValidationError(string? field, string? message)
    {
        Field = field ?? null;
        Message = message ?? null;
    }
}