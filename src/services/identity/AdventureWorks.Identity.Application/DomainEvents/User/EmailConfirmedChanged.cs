namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class EmailConfirmedChanged(bool isConfirmed)
{
    public bool IsConfirmed { get; set; } = isConfirmed;
}