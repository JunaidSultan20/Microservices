using System.Reflection;
using AdventureWorks.Common.Exceptions;

namespace AdventureWorks.Middlewares.Test.Exceptions;

public class ExceptionMiddlewareTest : ExceptionMiddlewareTestData
{
    [Fact]
    public async Task WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        // Arrange
        var sut = SetupMockMiddleware().Build();

        var context = CreateContext();

        // Act
        await sut.InvokeAsync(context);

        // Assert
        _mockRequestDelegate.Verify(x => x(context), Times.Once);
    }

    [Fact]
    public async Task WhenExceptionThrown_ShouldHandleAndReturnInternalServerError()
    {
        // Arrange
        var sut = SetupMockMiddlewareWhenExceptionThrown().Build();

        var context = CreateContext();

        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        // Act
        await sut.InvokeAsync(context);

        // Assert
        _mockRequestDelegate.Verify(x => x(context), Times.Once);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        context.Response.ContentType.Should().Be(Constants.ContentTypeJson);

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
        ApiException? response = JsonConvert.DeserializeObject<ApiException>(responseBody);
        response.Should().NotBeNull();
        response.Should().BeOfType(typeof(ApiException));
        
        Type type = typeof(ApiException);
        PropertyInfo? messageProperty = type.GetProperty("Message", BindingFlags.NonPublic | BindingFlags.Instance);
        PropertyInfo? detailsProperty = type.GetProperty("Details", BindingFlags.NonPublic | BindingFlags.Instance);

        string? messageValue = messageProperty?.GetValue(response) as string;
        string? detailsValue = detailsProperty?.GetValue(response) as string;

        messageValue.Should().Be("An error occurred while processing your request");
        detailsValue.Should().Be("Test Exception");
    }
}