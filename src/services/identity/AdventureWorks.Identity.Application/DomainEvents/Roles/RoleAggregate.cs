using AdventureWorks.Common.Events;

namespace AdventureWorks.Identity.Application.DomainEvents.Roles;

public class RoleAggregate : Aggregate
{
    public string Name { get; set; }

    public string NormalizedName { get; set; }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case RoleCreated role: 
                OnCreated(role);
                break;
            default:
                throw new ArgumentException($"Unsupported event type: {@event.GetType().Name}", nameof(@event));
        }
    }

    public void RoleCreatedEvent(string name) 
        => Apply(new RoleCreated(name, name.ToUpper()));

    private void OnCreated(RoleCreated @event)
    {
        Name = @event.Name;
        NormalizedName = @event.NormalizedName;
    }
}