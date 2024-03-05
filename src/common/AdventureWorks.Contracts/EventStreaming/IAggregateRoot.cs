using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AdventureWorks.Contracts.EventStreaming;

public interface IAggregateRoot
{
    long Version { get; }

    IReadOnlyCollection<IDomainEvent> Events { get; }
    
    void ClearEvents();
}