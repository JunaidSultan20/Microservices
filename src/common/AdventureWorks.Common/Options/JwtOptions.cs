namespace AdventureWorks.Common.Options;

/// <summary>
/// Represents the configuration options for JWT (JSON Web Token) authentication.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Gets or sets the issuer of the JWT.
    /// This property is required and cannot be an empty string.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Issuer is required and cannot be empty.")]
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the audience for which the JWT is intended.
    /// This property is required and cannot be an empty string.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Audience is required and cannot be empty.")]
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the secret key used for signing the JWT.
    /// This property is required and cannot be an empty string.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Secret is required and cannot be empty.")]
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiration time of the JWT in minutes.
    /// This property is required and must have a positive value.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "ExpirationMinutes is required and must be greater than zero.")]
    public double ExpirationMinutes { get; init; }
}