using AdventureWorks.Common.Constants;
using System.Net;
using System.Net.Http.Headers;
using AdventureWorks.Common.Helpers;
using AdventureWorks.Common.Response;

namespace AdventureWorks.Common.Test.Helpers;

public class HelperMethodsTest
{
    [Theory]
    [InlineData("application/json", true)]
    [InlineData("invalid-media-type", false)]
    public void CheckIfMediaTypeIsValid_ShouldReturnCorrectResult_ForApiResult(string mediaType, bool expectedIsValid)
    {
        // Act
        var result = HelperMethods.CheckIfMediaTypeIsValid<TestResponse>(mediaType, out MediaTypeHeaderValue? parsedMediaType, out ApiResult? responseValue);

        // Assert
        result.Should().Be(expectedIsValid);
        if (expectedIsValid)
        {
            parsedMediaType.Should().NotBeNull();
            responseValue.Should().BeNull();
        }
        else
        {
            parsedMediaType.Should().BeNull();
            responseValue.Should().NotBeNull();
            responseValue!.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseValue.Message.Should().Be(Messages.InvalidMediaType);
        }
    }

    [Theory]
    [InlineData("application/json", true)]
    [InlineData("invalid-media-type", false)]
    public void CheckIfMediaTypeIsValid_ShouldReturnCorrectResult_ForPagedApiResponse(string mediaType, bool expectedIsValid)
    {
        // Act
        var result = HelperMethods.CheckIfMediaTypeIsValid<TestResponse>(mediaType, out MediaTypeHeaderValue? parsedMediaType, out PagedApiResponse<TestResponse>? responseValue);

        // Assert
        result.Should().Be(expectedIsValid);
        if (expectedIsValid)
        {
            parsedMediaType.Should().NotBeNull();
            responseValue.Should().BeNull();
        }
        else
        {
            parsedMediaType.Should().BeNull();
            responseValue.Should().NotBeNull();
            responseValue!.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseValue.Message.Should().Be(Messages.InvalidMediaType);
        }
    }

    [Fact]
    public void GetInternalProperty_ShouldReturnCorrectValue()
    {
        // Arrange
        var testClass = new TestClass();

        // Act
        var result = HelperMethods.GetInternalProperty<string>(testClass, "InternalProperty");

        // Assert
        result.Should().Be("Internal Value");
    }

    private class TestClass
    {
        internal string InternalProperty { get; } = "Internal Value";
    }

    private class TestResponse { }
}