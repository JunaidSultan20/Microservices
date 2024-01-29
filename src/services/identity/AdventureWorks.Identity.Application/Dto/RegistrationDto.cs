namespace AdventureWorks.Identity.Application.Dto;

public record RegistrationDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public RegistrationDto()
    {
    }

    /// <summary>
    /// Parameterized constructor that takes values and assigns them to the properties
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="roles"></param>
    public RegistrationDto(string userName,
                           string email,
                           string password,
                           List<string> roles) => (UserName,
                                                   Email,
                                                   Password,
                                                   Roles) = (userName,
                                                             email,
                                                             password,
                                                             roles);
}