namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class BadRequestPostRegisterResponseExample : IMultipleExamplesProvider<BadRequestPostRegisterResponse>
{
    public IEnumerable<SwaggerExample<BadRequestPostRegisterResponse>> GetExamples()
    {
        return new List<SwaggerExample<BadRequestPostRegisterResponse>>
        {
            new SwaggerExample<BadRequestPostRegisterResponse>
            {
                Name = Messages.UnableToCreateUser,
                Summary = Messages.UnableToCreateUser,
                Value = new BadRequestPostRegisterResponse(Messages.UnableToCreateUser)
            },
            new SwaggerExample<BadRequestPostRegisterResponse>
            {
                Name = Messages.UnableToAssignRole,
                Summary = Messages.UnableToAssignRole,
                Value = new BadRequestPostRegisterResponse(Messages.UnableToAssignRole)
            }
        };
    }
}