using AdventureWorks.Common.Events;

namespace AdventureWorks.Identity.Application.DomainEvents.Roles;

/// <summary>
/// Represents an aggregate for managing role creation events in the domain.
/// </summary>
public class RoleAggregate : Aggregate
{
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the normalized name of the role, typically an uppercase version of the role name.
    /// </summary>
    public string NormalizedName { get; set; }

    /// <summary>
    /// Handles the application of domain events to the aggregate state.
    /// </summary>
    /// <param name="event">The event to apply to the aggregate.</param>
    /// <exception cref="ArgumentException">Thrown when an unsupported event type is encountered.</exception>
    protected override void When(object @event)
    {
        switch (@event)
        {
            case RoleCreated role: 
                OnCreated(role);
                break;
            default:
                throw new ArgumentException($"Unsupported event type: { @event.GetType().Name }", nameof(@event));
        }
    }

    /// <summary>
    /// Triggers the creation of a role by applying a <see cref="RoleCreated"/> event.
    /// </summary>
    /// <param name="name">The name of the role to be created.</param>
    public void RoleCreatedEvent(string name) 
        => Apply(new RoleCreated(name));

    /// <summary>
    /// Updates the aggregate's state when a <see cref="RoleCreated"/> event is applied.
    /// </summary>
    /// <param name="event">The <see cref="RoleCreated"/> event that contains the role creation details.</param>
    private void OnCreated(RoleCreated @event)
    {
        Name = @event.Name;
        NormalizedName = @event.NormalizedName;
    }
}