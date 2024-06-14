namespace AdventureWorks.Contracts.EventStreaming;

public interface IDomainEvent
{
    public long AggregateVersion { get; }

    string AggregateId { get; }

    DateTime TimeStamp { get; }
}