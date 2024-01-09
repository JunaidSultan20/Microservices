namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class PostRegisterResponse : ApiResponse<UserDto>
{
    public PostRegisterResponse(HttpStatusCode statusCode, string? message) : base(statusCode, message)
    {
    }

    public PostRegisterResponse(HttpStatusCode statusCode, string? message, UserDto? result) :
                           this(statusCode, message) => Result = result;
}