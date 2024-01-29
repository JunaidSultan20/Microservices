namespace AdventureWorks.Identity.Application.Dto;

public record AuthenticationDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public AuthenticationDto()
    {
    }

    /// <summary>
    /// Parameterized constructor that takes two variables and assigns them to Email and Password properties 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public AuthenticationDto(string? email, string? password) => (Email, Password) = (email, password);
}