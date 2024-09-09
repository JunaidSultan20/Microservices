namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class NotFoundPostRegisterResponseExample : IExamplesProvider<NotFoundPostRegisterResponse>
{
    public NotFoundPostRegisterResponse GetExamples()
    {
        return new NotFoundPostRegisterResponse(Messages.RoleNotFound);
    }
}