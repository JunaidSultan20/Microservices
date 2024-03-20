namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class AccessFailedCountChanged(int oldCount, int newCount)
{
    public int OldCount { get; set; } = oldCount;

    public int NewCount { get; set; } = newCount;
}