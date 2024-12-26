namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a user being created.
/// </summary>
/// <remarks>
/// This class provides information about the newly created user, including their username, email, password, and role.
/// </remarks>
/// <param name="username">The username of the created user.</param>
/// <param name="email">The email address of the created user.</param>
/// <param name="password">The password of the created user.</param>
/// <param name="role">The role assigned to the created user.</param>
public record UserCreated(string username, string email, string password, string role)
{
    /// <summary>
    /// Gets or sets the username of the created user.
    /// </summary>
    public string Username { get; set; } = username;

    /// <summary>
    /// Gets or sets the email address of the created user.
    /// </summary>
    public string Email { get; set; } = email;

    /// <summary>
    /// Gets or sets the password of the created user.
    /// </summary>
    public string Password { get; set; } = password;

    /// <summary>
    /// Gets or sets the role assigned to the created user.
    /// </summary>
    public string Role { get; set; } = role;
}