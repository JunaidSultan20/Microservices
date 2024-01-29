using ValidationException = AdventureWorks.Common.Exceptions.ValidationException;

namespace AdventureWorks.Common.Filters;

public class ModelValidationFilter : IAsyncActionFilter
{
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