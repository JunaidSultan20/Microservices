namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class ConflictPostRegisterResponse() : PostRegisterResponse(HttpStatusCode.Conflict, Messages.UserExists);