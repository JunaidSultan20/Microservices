using AdventureWorks.Common.Events;
using AdventureWorks.Identity.Application.DomainEvents.User;

namespace AdventureWorks.Identity.Application.DomainEvents;

public class UserAggregate : Aggregate
{
    public string Username { get; set; }

    public string NormalizedUsername { get; set; }

    public string Email { get; set; }

    public string NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string Role { get; set; }

    public int RoleId { get; set; }

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

    public void UserCreatedEvent(string username,
                                 string email,
                                 string password,
                                 string role)
        => Apply(new UserCreated(username, email, password, role));

    public void PasswordChangedEvent(string oldPasswordHash,
                                     string newPasswordHash)
        => Apply(new PasswordChanged(oldPasswordHash, newPasswordHash));

    public void EmailChangedEvent(string oldEmail,
                                  string newEmail)
        => Apply(new EmailChanged(oldEmail, newEmail));

    public void EmailConfirmedChangedEvent(bool isConfirmed)
        => Apply(new EmailConfirmedChanged(isConfirmed));

    public void PhoneNumberChangedEvent(string oldPhoneNumber, string newPhoneNumber)
        => Apply(new PhoneNumberChanged(oldPhoneNumber, newPhoneNumber));

    public void PhoneNumberConfirmedEvent(bool isConfirmed)
        => Apply(new PhoneNumberConfirmedChanged(isConfirmed));

    public void TwoFactorEnabledChangedEvent(bool isConfirmed)
        => Apply(new TwoFactorEnabledChanged(isConfirmed));

    public void LockoutEndChangedEvent(DateTime? oldLockoutEnd, DateTime? newLockoutEnd)
        => Apply(new LockoutEndChanged(oldLockoutEnd, newLockoutEnd));

    public void LockoutEnabledChangedEvent(bool isConfirmed)
        => Apply(new LockoutEnabledChanged(isConfirmed));

    public void AccessFailedCountChangedEvent(int oldCount, int newCount)
        => Apply(new AccessFailedCountChanged(oldCount, newCount));

    public void UserRoleChangedEvent(int? oldRole, int newRole)
        => Apply(new UserRoleChanged(oldRole, newRole));

    private void OnCreated(UserCreated @event)
    {
        Username = @event.Username;
        Email = @event.Email;
        Password = @event.Password;
        Role = @event.Role;
    }

    private void OnPasswordChanged(PasswordChanged @event) => Password = @event.NewPasswordHash;

    private void OnEmailChanged(EmailChanged @event) => Email = @event.NewEmail;

    private void OnEmailConfirmedChanged(EmailConfirmedChanged @event) => EmailConfirmed = @event.IsConfirmed;

    private void OnPhoneNumberChanged(PhoneNumberChanged @event) => PhoneNumber = @event.NewPhoneNumber;

    private void OnPhoneNumberConfirmed(PhoneNumberConfirmedChanged @event) => PhoneNumberConfirmed = @event.IsConfirmed;

    private void OnTwoFactorEnabledChanged(TwoFactorEnabledChanged @event) => TwoFactorEnabled = @event.IsConfirmed;

    private void OnLockoutEndChanged(LockoutEndChanged @event) => LockoutEnd = @event.NewLockoutEnd;

    private void OnLockoutEnabledChanged(LockoutEnabledChanged @event) => LockoutEnabled = @event.LockoutChanged;

    private void OnAccessFailedCountChanged(AccessFailedCountChanged @event) => AccessFailedCount = @event.NewCount;

    private void OnUserRoleChanged(UserRoleChanged @event) => RoleId = @event.NewRole;
}