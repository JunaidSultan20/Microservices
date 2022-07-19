namespace Identity.Application.DTOs;

public class RegistrationDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<string>? Roles { get; set; }
}