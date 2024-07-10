using AdventureWorks.Common.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Common.Test.Filters;

public class RequestHeaderFilterTest
{
    [Fact]
    public void OnActionExecuting_ShouldAddRemoteIpAddressToHttpContextItems_WhenHeaderExists()
    {
        // Arrange
        var headerKey = "X-Forwarded-For";
        var headerValue = "192.168.1.1";
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers[headerKey] = headerValue;

        var actionContext = new ActionContext(
            httpContext,
            new Microsoft.AspNetCore.Routing.RouteData(),
            new ControllerActionDescriptor());

        var actionExecutingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            new Mock<ControllerBase>().Object);

        var filter = new RequestHeaderFilter();

        // Act
        filter.OnActionExecuting(actionExecutingContext);

        // Assert
        httpContext.Items.ContainsKey(Common.Constants.Constants.RemoteIpAddress).Should().BeTrue();
        httpContext.Items[Common.Constants.Constants.RemoteIpAddress].Should().Be(headerValue);
    }

    [Fact]
    public void OnActionExecuting_ShouldNotAddRemoteIpAddressToHttpContextItems_WhenHeaderDoesNotExist()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();

        var actionContext = new ActionContext(
            httpContext,
            new Microsoft.AspNetCore.Routing.RouteData(),
            new ControllerActionDescriptor());

        var actionExecutingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            new Mock<ControllerBase>().Object);

        var filter = new RequestHeaderFilter();

        // Act
        filter.OnActionExecuting(actionExecutingContext);

        // Assert
        httpContext.Items.ContainsKey(Common.Constants.Constants.XPaginationKey).Should().BeFalse();
    }
}