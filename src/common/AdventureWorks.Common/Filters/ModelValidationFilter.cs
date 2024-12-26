using ValidationException = AdventureWorks.Common.Exceptions.ValidationException;

namespace AdventureWorks.Common.Filters;

/// <summary>
/// A filter that performs model validation before an action is executed.
/// If the model state is invalid, it returns a 422 Unprocessable Entity response with validation errors.
/// </summary>
public class ModelValidationFilter : IAsyncActionFilter
{
    /// <summary>
    /// Called before an action is executed to check the validity of the model state.
    /// If the model state is invalid, it returns a validation error response.
    /// </summary>
    /// <param name="context">The context in which the action is executed, containing information about the request and the model state.</param>
    /// <param name="next">The delegate representing the next action to execute if the model state is valid.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

            ValidationException response = new ValidationException(errors: context.ModelState.Keys.SelectMany(selector: key
                                                                       => context.ModelState[key]?.Errors.Select(x
                                                                              => new ValidationError(key, x.ErrorMessage)) ??
                                                                          Array.Empty<ValidationError>()).ToList().AsReadOnly());

            context.Result = new UnprocessableEntityObjectResult(response);
            return;
        }

        await next();
    }
}