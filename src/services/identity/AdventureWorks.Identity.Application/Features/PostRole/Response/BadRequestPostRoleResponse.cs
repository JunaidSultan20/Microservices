namespace AdventureWorks.Identity.Application.Features.PostRole.Response;

public class BadRequestPostRoleResponse(string message) : PostRoleResponse(HttpStatusCode.BadRequest, message);