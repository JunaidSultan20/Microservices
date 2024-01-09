namespace AdventureWorks.Identity.Application.Dto;

public class LoginDto
{
    public string? Token { get; }
    public DateTime? Expiration { get; }
    public string? RefreshToken { get; }
    public DateTime? RefreshTokenExpiration { get; }

    public LoginDto()
    {
    }

    public LoginDto(string? token,
                    DateTime? expiration,
                    string? refreshToken,
                    DateTime? refreshTokenExpiration) => (Token,
                                                          Expiration,
                                                          RefreshToken,
                                                          RefreshTokenExpiration) = (token,
                                                                                     expiration,
                                                                                     refreshToken,
                                                                                     refreshTokenExpiration);
}