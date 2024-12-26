namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

/// <summary>
/// Represents the response for a conflict during a post-role operation.
/// </summary>
/// <remarks>
/// This class extends PostRoleResponse and provides a specific response for conflicts.
/// It sets the HTTP status code to Conflict and the message to indicate a duplicate record error.
/// </remarks>
public class ConflictPostRoleResponse() : PostRoleResponse(HttpStatusCode.Conflict, Messages.DuplicateRecordError);