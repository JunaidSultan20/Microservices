namespace AdventureWorks.Identity.Application.Features.ChangePassword.Response;

/// <summary>
/// Represents the response for bad request when requesting for password change.
/// </summary>
/// <remarks>
/// This class extends ChangePasswordResponse and provides a specific response for bad request.
/// It sets the HTTP status code to Bad Request and the message to indicate the error.
/// </remarks>
public class BadRequestChangePasswordResponse() : ChangePasswordResponse(HttpStatusCode.BadRequest, Messages.UnableToChangePassword);