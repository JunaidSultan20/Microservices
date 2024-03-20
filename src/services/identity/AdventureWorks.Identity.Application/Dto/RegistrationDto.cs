namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// Parameterized constructor that takes values and assigns them to the properties
/// </summary>
/// <param name="Username"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
/// <param name="Role"></param>
public record RegistrationDto(string Username, string Email, string Password, string Role)
{
    public string Username { get; set; } = Username;

    public string Email { get; set; } = Email;

    public string Password { get; set; } = Password;

    public string Role { get; set; } = Role;
}