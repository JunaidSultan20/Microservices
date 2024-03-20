namespace AdventureWorks.Identity.Application.Dto;

public record UserDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }

    public UserDto()
    {
    }

    public UserDto(string? userName, string? email) => (UserName, Email) = (userName, email);
}