namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

public class ConflictPostRoleResponse() : PostRoleResponse(HttpStatusCode.Conflict, Messages.DuplicateRecordError);