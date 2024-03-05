namespace AdventureWorks.Common.Events;

public class Event(string id, string type, string streamId, long version, object data)
{
    public string Id { get; private set; } = id;

    public string Type { get; private set; } = type;

    public string StreamId { get; private set; } = string.Empty;

    public long Version { get; private set; } = version;

    public DateTime TimeStamp { get; set; } = DateTime.Now;

    public object Data { get; set; } = data;
}