namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class NotFoundPostRegisterResponse(string message) : PostRegisterResponse(HttpStatusCode.NotFound, message);