namespace AdventureWorks.Identity.Application.Dto;

/// <summary>
/// The registration data transfer object that contains the properties to create the user.
/// </summary>
/// <param name="Username" example="jon.doe"></param>
/// <param name="Email" example="jon.doe@example.com"></param>
/// <param name="Password" example="jonDoe@123"></param>
/// <param name="Role" example="User"></param>
public record RegistrationDto(string Username, string Email, string Password, string Role);
//{
    ///// <summary>
    ///// Gets or sets the username property.
    ///// </summary>
    ///// <example>jon.doe</example>
    //public string Username { get; set; } = username;

    ///// <summary>
    ///// Gets or sets the email property.
    ///// </summary>
    ///// <example>jon.doe@xyz.com</example>
    //public string Email { get; set; } = email;

    ///// <summary>
    ///// Gets or sets the password property.
    ///// </summary>
    ///// <example>jonDoe@123</example>
    //public string Password { get; set; } = password;

    ///// <summary>
    ///// Gets or sets the role property.
    ///// </summary>
    ///// <example>User</example>
    //public string Role { get; set; } = role;
//}