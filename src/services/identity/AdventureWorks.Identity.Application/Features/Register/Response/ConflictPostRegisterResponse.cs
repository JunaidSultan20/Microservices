namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class ConflictPostRegisterResponse : PostRegisterResponse
{
    public ConflictPostRegisterResponse() : base(HttpStatusCode.Conflict, Messages.UserExists)
    {
    }
}