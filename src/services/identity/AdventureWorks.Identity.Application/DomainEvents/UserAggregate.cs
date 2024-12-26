using AdventureWorks.Common.Events;
using AdventureWorks.Identity.Application.DomainEvents.User;

namespace AdventureWorks.Identity.Application.DomainEvents;

/// <summary>
/// Represents the User Aggregate in the context of the Event Sourcing pattern.
/// This class manages user data and applies changes based on different user-related events.
/// </summary>
public class UserAggregate : Aggregate
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the username in a normalized format (e.g., uppercase).
    /// </summary>
    public string NormalizedUsername { get; set; }

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the email address in a normalized format (e.g., lowercase).
    /// </summary>
    public string NormalizedEmail { get; set; }

    /// <summary>
    /// Gets or sets whether the user's email address has been confirmed.
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the user's password hash (for security reasons, the actual password is not stored).
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the user's phone number (if provided).
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets whether the user's phone number has been confirmed.
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Gets or sets whether two-factor authentication is enabled for the user.
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user's lockout period ends (null if not locked out).
    /// </summary>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets whether user account lockout is enabled.
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the number of failed login attempts since the last successful login.
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the user's assigned role name.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user's assigned role (might be more efficient than storing the role name).
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// This method applies the provided event to the user's state. It's called internally by the event-related methods and the When method.
    /// </summary>
    /// <param name="event">The event object representing the change to be applied.</param>
    protected override void When(object @event)
    {
        switch (@event)
        {
            case UserCreated user:
                OnCreated(user);
                break;

            case PasswordChanged user:
                OnPasswordChanged(user);
                break;

            case EmailChanged user:
                OnEmailChanged(user);
                break;

            case EmailConfirmedChanged user:
                OnEmailConfirmedChanged(user);
                break;

            case PhoneNumberChanged user:
                OnPhoneNumberChanged(user);
                break;

            case PhoneNumberConfirmedChanged user:
                OnPhoneNumberConfirmed(user);
                break;

            case TwoFactorEnabledChanged user:
                OnTwoFactorEnabledChanged(user);
                break;

            case LockoutEndChanged user:
                OnLockoutEndChanged(user);
                break;

            case LockoutEnabledChanged user:
                OnLockoutEnabledChanged(user);
                break;

            case AccessFailedCountChanged user:
                OnAccessFailedCountChanged(user);
                break;

            case UserRoleChanged user:
                OnUserRoleChanged(user);
                break;

            default:
                throw new ArgumentException($"Unsupported event type: {@event.GetType().Name}", nameof(@event));
        }
    }

    /// <summary>
    /// Applies a UserCreated event to the user's state.
    /// </summary>
    /// <param name="username">The username of the created user.</param>
    /// <param name="email">The email address of the created user.</param>
    /// <param name="password">The password of the created user.</param>
    /// <param name="role">The role assigned to the created user.</param>
    public void UserCreatedEvent(string username, string email, string password, string role) 
        => Apply(new UserCreated(username, email, password, role));

    /// <summary>
    /// Applies a PasswordChanged event to the user's state.
    /// </summary>
    /// <param name="oldPasswordHash">The hash of the original password.</param>
    /// <param name="newPasswordHash">The hash of the new password.</param>
    public void PasswordChangedEvent(string oldPasswordHash, string newPasswordHash)
        => Apply(new PasswordChanged(oldPasswordHash, newPasswordHash));

    /// <summary>
    /// Applies an EmailChanged event to the user's state.
    /// </summary>
    /// <param name="oldEmail">The original email address.</param>
    /// <param name="newEmail">The new email address.</param>
    public void EmailChangedEvent(string oldEmail, string newEmail)
        => Apply(new EmailChanged(oldEmail, newEmail));

    /// <summary>
    /// Applies an EmailConfirmedChanged event to the user's state.
    /// </summary>
    /// <param name="isConfirmed">Whether the email address is confirmed.</param>
    public void EmailConfirmedChangedEvent(bool isConfirmed)
        => Apply(new EmailConfirmedChanged(isConfirmed));
    /// <summary>
    /// Applies a PhoneNumberChanged event to the user's state.
    /// </summary>
    /// <param name="oldPhoneNumber">The original phone number.</param>
    /// <param name="newPhoneNumber">The new phone number.</param>
    public void PhoneNumberChangedEvent(string oldPhoneNumber, string newPhoneNumber)
        => Apply(new PhoneNumberChanged(oldPhoneNumber, newPhoneNumber));

    /// <summary>
    /// Applies a PhoneNumberConfirmed event to the user's state.
    /// </summary>
    /// <param name="isConfirmed">Whether the phone number is confirmed.</param>
    public void PhoneNumberConfirmedEvent(bool isConfirmed)
        => Apply(new PhoneNumberConfirmedChanged(isConfirmed));

    /// <summary>
    /// Applies a TwoFactorEnabledChanged event to the user's state.
    /// </summary>
    /// <param name="isConfirmed">Whether two-factor authentication is confirmed.</param>
    public void TwoFactorEnabledChangedEvent(bool isConfirmed)
        => Apply(new TwoFactorEnabledChanged(isConfirmed));

    /// <summary>
    /// Applies a LockoutEndChanged event to the user's state.
    /// </summary>
    /// <param name="oldLockoutEnd">The original lockout end time.</param>
    /// <param name="newLockoutEnd">The new lockout end time.</param>
    public void LockoutEndChangedEvent(DateTime? oldLockoutEnd, DateTime? newLockoutEnd)
        => Apply(new LockoutEndChanged(oldLockoutEnd, newLockoutEnd));

    /// <summary>
    /// Applies a LockoutEnabledChanged event to the user's state.
    /// </summary>
    /// <param name="isConfirmed">Whether the lockout status has been changed.</param>
    public void LockoutEnabledChangedEvent(bool isConfirmed)
        => Apply(new LockoutEnabledChanged(isConfirmed));
    /// <summary>
    /// Applies an AccessFailedCountChanged event to the user's state.
    /// </summary>
    /// <param name="oldCount">The original access failed count.</param>
    /// <param name="newCount">The new access failed count.</param>
    public void AccessFailedCountChangedEvent(int oldCount, int newCount)
        => Apply(new AccessFailedCountChanged(oldCount, newCount));

    /// <summary>
    /// Applies a UserRoleChanged event to the user's state.
    /// </summary>
    /// <param name="oldRole">The ID of the user's original role.</param>
    /// <param name="newRole">The ID of the user's new role.</param>
    public void UserRoleChangedEvent(int? oldRole, int newRole)
        => Apply(new UserRoleChanged(oldRole, newRole));

    /// <summary>
    /// Applies the user information from a UserCreated event to the user's state.
    /// </summary>
    /// <param name="event">The UserCreated event containing the user information.</param>
    private void OnCreated(UserCreated @event)
    {
        Username = @event.Username;
        Email = @event.Email;
        Password = @event.Password;
        Role = @event.Role;
    }

    /// <summary>
    /// Updates the user's password with the new password hash from a PasswordChanged event.
    /// </summary>
    /// <param name="event">The PasswordChanged event containing the new password hash.</param>
    private void OnPasswordChanged(PasswordChanged @event) => Password = @event.NewPasswordHash;

    /// <summary>
    /// Updates the user's email with the new email from an EmailChanged event.
    /// </summary>
    /// <param name="event">The EmailChanged event containing the new email address.</param>
    private void OnEmailChanged(EmailChanged @event) => Email = @event.NewEmail;

    /// <summary>
    /// Updates the user's email confirmation status based on an EmailConfirmedChanged event.
    /// </summary>
    /// <param name="event">The EmailConfirmedChanged event indicating the confirmation status.</param>
    private void OnEmailConfirmedChanged(EmailConfirmedChanged @event) => EmailConfirmed = @event.IsConfirmed;

    /// <summary>
    /// Updates the user's phone number with the new phone number from a PhoneNumberChanged event.
    /// </summary>
    /// <param name="event">The PhoneNumberChanged event containing the new phone number.</param>
    private void OnPhoneNumberChanged(PhoneNumberChanged @event) => PhoneNumber = @event.NewPhoneNumber;

    /// <summary>
    /// Updates the user's phone number confirmation status based on a PhoneNumberConfirmedChanged event.
    /// </summary>
    /// <param name="event">The PhoneNumberConfirmedChanged event indicating the confirmation status.</param>
    private void OnPhoneNumberConfirmed(PhoneNumberConfirmedChanged @event) => PhoneNumberConfirmed = @event.IsConfirmed;

    /// <summary>
    /// Updates the user's two-factor authentication status based on a TwoFactorEnabledChanged event.
    /// </summary>
    /// <param name="event">The TwoFactorEnabledChanged event indicating the enabled/disabled status.</param>
    private void OnTwoFactorEnabledChanged(TwoFactorEnabledChanged @event) => TwoFactorEnabled = @event.IsConfirmed;

    /// <summary>
    /// Updates the user's lockout end time based on a LockoutEndChanged event.
    /// </summary>
    /// <param name="event">The LockoutEndChanged event containing the new lockout end time.</param>
    private void OnLockoutEndChanged(LockoutEndChanged @event) => LockoutEnd = @event.NewLockoutEnd;

    /// <summary>
    /// Updates the user's lockout enabled status based on a LockoutEnabledChanged event.
    /// </summary>
    /// <param name="event">The LockoutEnabledChanged event indicating the enabled/disabled status.</param>
    private void OnLockoutEnabledChanged(LockoutEnabledChanged @event) => LockoutEnabled = @event.LockoutChanged;

    /// <summary>
    /// Updates the user's access failed count based on an AccessFailedCountChanged event.
    /// </summary>
    /// <param name="event">The AccessFailedCountChanged event containing the new access failed count.</param>
    private void OnAccessFailedCountChanged(AccessFailedCountChanged @event) => AccessFailedCount = @event.NewCount;

    /// <summary>
    /// Updates the user's role ID with the new role ID from a UserRoleChanged event.
    /// </summary>
    /// <param name="event">The UserRoleChanged event containing the new role ID.</param>
    private void OnUserRoleChanged(UserRoleChanged @event) => RoleId = @event.NewRole;
}