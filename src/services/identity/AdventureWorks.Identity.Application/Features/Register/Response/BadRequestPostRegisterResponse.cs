namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class BadRequestPostRegisterResponse(string message) : PostRegisterResponse(HttpStatusCode.BadRequest, message);