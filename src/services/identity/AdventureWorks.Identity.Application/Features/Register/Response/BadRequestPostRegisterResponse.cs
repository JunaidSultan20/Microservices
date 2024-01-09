namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class BadRequestPostRegisterResponse : PostRegisterResponse
{
    public BadRequestPostRegisterResponse(string message) : base(HttpStatusCode.BadRequest, message)
    {
    }
}