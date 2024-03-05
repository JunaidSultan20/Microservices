namespace AdventureWorks.Identity.Application.Features.Register.Response;

public class PostRegisterResponse(HttpStatusCode statusCode, 
                                  string? message) : ApiResponse<UserDto>(statusCode, 
                                                                          message)
{
    public PostRegisterResponse(HttpStatusCode statusCode, 
                                string? message, 
                                UserDto? result) : this(statusCode, 
                                                        message) => Result = result;
}