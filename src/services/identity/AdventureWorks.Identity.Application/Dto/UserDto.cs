namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// The user data transfer object with properties to manage the user details.
/// </summary>
/// <param name="Username" example="jane.doe"></param>
/// <param name="Email" example="jane.doe@example.com"></param>
public record UserDto(string? Username, string? Email);