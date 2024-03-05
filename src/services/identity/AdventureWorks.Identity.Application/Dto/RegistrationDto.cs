namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// Parameterized constructor that takes values and assigns them to the properties
/// </summary>
/// <param name="username"></param>
/// <param name="email"></param>
/// <param name="password"></param>
/// <param name="role"></param>
public record RegistrationDto(string username, string email, string password, string role)
{
    public string Username { get; set; } = username;

    public string Email { get; set; } = email;

    public string Password { get; set; } = password;

    public string Role { get; set; } = role;
}