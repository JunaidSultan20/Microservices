namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// Parameterized constructor that takes two variables and assigns them to email and Password properties 
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record AuthenticationDto(string? Email, string? Password)
{
}