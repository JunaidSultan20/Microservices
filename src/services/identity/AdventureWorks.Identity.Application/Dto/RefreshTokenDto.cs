namespace AdventureWorks.Identity.Application.Dto;

public record RefreshTokenDto
{
    public string? Token { get; }
    public string? RefreshToken { get; }

    public RefreshTokenDto()
    {
    }

    public RefreshTokenDto(string? token, string? refreshToken) => (Token, RefreshToken) = (token, refreshToken);
}