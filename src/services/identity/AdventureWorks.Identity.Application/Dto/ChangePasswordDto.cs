namespace AdventureWorks.Identity.Application.Dto;

public class ChangePasswordDto
{
    public string? Email { get; }
    public string? CurrentPassword { get; }
    public string? NewPassword { get; }

    public ChangePasswordDto()
    {
    }

    public ChangePasswordDto(string? email, string? currentPassword, string? newPassword) => (Email,
                                                                                              CurrentPassword,
                                                                                              NewPassword) = (email,
                                                                                                              currentPassword,
                                                                                                              newPassword);
}