using System.ComponentModel.DataAnnotations;
using AdventureWorks.Common.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AdventureWorks.Common.Constants;
using ValidationException = AdventureWorks.Common.Exceptions.ValidationException;

namespace AdventureWorks.Common.Test.Filters;

public class ModelValidationFilterTest
{
    [Theory]
    [InlineData("FirstName", "First name is required")]
    [InlineData("LastName", "Last name is required")]
    [InlineData("Age", "Age should be greater than 18")]
    [InlineData("Range", "Range should be between 100 and 150")]
    public async Task OnActionExecutionAsync_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid(string key, string errorMessage)
    {
        // Arrange
        var sampleUser = new TestUser
        {
            FirstName = key == "FirstName" ? null : "John",
            LastName = key == "LastName" ? null : "Doe",
            Age = key == "Age" ? 17 : 25,
            Range = key == "Range" ? 99 : 120
        };

        var validationContext = new ValidationContext(sampleUser);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(sampleUser, validationContext, validationResults, true);

        var modelState = new ModelStateDictionary();
        foreach (var validationResult in validationResults)
        {
            foreach (var memberName in validationResult.MemberNames)
            {
                modelState.AddModelError(memberName, validationResult.ErrorMessage);
            }
        }

        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, 
                                              new Microsoft.AspNetCore.Routing.RouteData(), 
                                              new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor(), 
                                              modelState);

        var actionExecutingContext = new ActionExecutingContext(actionContext, 
                                                                new List<IFilterMetadata>(), 
                                                                new Dictionary<string, object>(), 
                                                                new Mock<ControllerBase>().Object);
        var actionExecutionDelegate = new Mock<ActionExecutionDelegate>();

        var filter = new ModelValidationFilter();

        // Act
        await filter.OnActionExecutionAsync(actionExecutingContext, actionExecutionDelegate.Object);

        // Assert
        httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.UnprocessableEntity);
        actionExecutingContext.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

        var result = actionExecutingContext.Result as UnprocessableEntityObjectResult;
        result.Should().NotBeNull();

        var response = result!.Value as ValidationException;
        response.Should().NotBeNull();
        response!.IsSuccessful.Should().BeFalse();
        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response!.Message.Should().Be(Messages.ValidationError);
        response!.Errors.Should().HaveCount(1);
        response!.Errors?.FirstOrDefault()?.Field.Should().Be(key);
        response!.Errors?.FirstOrDefault()?.Message.Should().Be(errorMessage);
    }

    [Fact]
    public async Task OnActionExecutionAsync_ShouldCallNext_WhenModelStateIsValid()
    {
        // Arrange
        var modelState = new ModelStateDictionary();

        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor(), modelState);

        var actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object>(), new Mock<ControllerBase>().Object);
        var actionExecutionDelegate = new Mock<ActionExecutionDelegate>();

        var filter = new ModelValidationFilter();

        // Act
        await filter.OnActionExecutionAsync(actionExecutingContext, actionExecutionDelegate.Object);

        // Assert
        actionExecutionDelegate.Verify(aed => aed(), Times.Once);
    }

    private class TestUser
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Range(18, int.MaxValue, ErrorMessage = "Age should be greater than 18")]
        public int Age { get; set; }

        [Range(100, 150, ErrorMessage = "Range should be between 100 and 150")]
        public int Range { get; set; }
    }
}