using AdventureWorks.Common.Response;

namespace AdventureWorks.Middlewares.Test.Headers;

public class AcceptHeaderMiddlewareTest : AcceptHeaderMiddlewareTestData
{
    [Theory]
    [InlineData(Constants.ContentTypeJson)]
    [InlineData(Constants.ContentTypeJsonHateoas)]
    [InlineData(Constants.ContentTypeTextJson)]
    [InlineData(Constants.ContentTypeTextPlain)]
    [InlineData(Constants.ContentTypeXml)]
    [InlineData(Constants.ContentTypeXmlHateoas)]
    public async Task ValidAcceptHeader_ShouldCallNextDelegate(string header)
    {
        // Arrange
        var sut = SetupMockAcceptHeaderMiddleware().Build();

        var context = CreateContext();
        context.Request.Headers["Accept"] = header;

        // Act
        await sut.InvokeAsync(context);

        // Assert
        _mockNext.Verify(next => next(context), Times.Once);
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("invalid-media-type")]
    [InlineData("application+json")]
    public async Task InvalidAcceptHeader_ShouldReturnBadRequest(string header)
    {
        // Arrange
        var sut = SetupMockAcceptHeaderMiddleware().Build();

        var context = CreateContext();
        context.Request.Headers["Accept"] = header;

        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        // Act
        await sut.InvokeAsync(context);

        // Assert
        _mockNext.Verify(next => next(It.IsAny<HttpContext>()), Times.Never);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        context.Response.ContentType.Should().Be(Constants.ContentTypeJson);

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();

        //var expectedResponse = new ApiResult(HttpStatusCode.BadRequest, "Invalid media type");
        ApiResult? response = JsonConvert.DeserializeObject<ApiResult>(responseBody);

        response?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response?.IsSuccessful.Should().BeFalse();
        response?.Message.Should().Be(Messages.InvalidMediaType);
    }
}