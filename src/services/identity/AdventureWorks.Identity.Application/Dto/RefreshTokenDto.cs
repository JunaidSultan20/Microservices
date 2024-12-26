namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// The refresh token data transfer object with properties to refresh the expired access token.
/// </summary>
/// <param name="Token" example="accesstoken"></param>
/// <param name="RefreshToken" example="refreshtoken"></param>
public record RefreshTokenDto(string? Token, string? RefreshToken);