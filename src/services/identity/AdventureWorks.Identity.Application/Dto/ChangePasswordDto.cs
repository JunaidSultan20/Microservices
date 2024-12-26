namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// The change password data transfer object that contains properties to change the password of an account
/// </summary>
/// <param name="Email" example="jane.doe@xyz.com"></param>
/// <param name="CurrentPassword" example="janedoe@123"></param>
/// <param name="NewPassword" example="janeDoe@123"></param>
public record ChangePasswordDto(string? Email, string? CurrentPassword, string? NewPassword);