namespace AdventureWorks.Identity.Application.DomainEvents.User;

public class PhoneNumberConfirmedChanged(bool isConfirmed)
{
    public bool IsConfirmed { get; set; } = isConfirmed;
}