namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// The authentication data transfer object that contains properties to authenticate the user.
/// </summary>
/// <param name="Email" example="jane.doe@xyz.com"></param>
/// <param name="Password" example="janDoe@123"></param>
public record AuthenticationDto(string? Email, string? Password);