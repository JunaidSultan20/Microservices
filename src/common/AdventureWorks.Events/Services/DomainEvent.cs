using AdventureWorks.Contracts.EventStreaming;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AdventureWorks.Events.Services;

public class DomainEvent : IDomainEvent
{
    public DomainEvent()
    {
    }

    //Implementation of IDomainEvent
    public long AggregateVersion { get; private set; }

    //Implementation of IDomainEvent
    public string AggregateId { get; private set; }

    //Implementation of IDomainEvent
    public DateTime TimeStamp { get; private set; }
}