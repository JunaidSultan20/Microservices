namespace AdventureWorks.Identity.Application.DomainEvents.User;

public record UserCreated(string username, string email, string password, string role)
{
    public string Username { get; set; } = username;

    public string Email { get; set; } = email;

    public string Password { get; set; } = password;

    public string Role { get; set; } = role;
}