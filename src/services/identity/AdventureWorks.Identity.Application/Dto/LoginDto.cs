namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// The login data transfer object with properties to return the access and refresh tokens after successful authentication.
/// </summary>
/// <param name="Token" example="accessTokem"></param>
/// <param name="Expiration"  example="2025-01-31"></param>
/// <param name="RefreshToken" example="refreshToken"></param>
/// <param name="RefreshTokenExpiration" example="2025-02-31"></param>
public record LoginDto(string? Token, DateTime? Expiration, string? RefreshToken, DateTime? RefreshTokenExpiration);