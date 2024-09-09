using AdventureWorks.Identity.Application.Features.Login.Handler;
using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Application.Features.Login.Response;

namespace AdventureWorks.Identity.Test.Features.Login.Handler;

public class PostLoginTest : PostLoginTestData
{
    [Theory]
    [InlineData("tom.cruise@gmail.com", "tom.cruise")]
    [InlineData("test.user@outlook.com", "test.user")]
    public async Task When_Login_Is_Passed_Expect_Token_Is_Returned(string email, string password)
    {
        // Arrange
        PostLoginHandler sut = SetupMockOptions()
                              .SetupMockHttpContext()
                              .SetupMockFindByEmail(true)
                              .SetupMockCheckUserPassword(true)
                              .SetupMockGetRoles()
                              .SetupMockGetAuthenticationToken(true)
                              .SetupMockRemoveAuthenticationToken(true)
                              .SetupMockGenerateUserToken()
                              .SetupMockSetAuthenticationToken(true)
                              .Build();

        PostLoginRequest request = CreateRequest(email, password);

        // Act
        PostLoginResponse response = await sut.Handle(request, CancellationToken.None);

        // Assert
        response.IsSuccessful.Should().BeTrue();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Message.Should().Be(Messages.BearerTokenGenerated);

        // Verification
        var httpContext = MockHttpContextAccessor.Object.HttpContext;

        httpContext?.Response.Headers.SetCookie.Should().NotBeNullOrEmpty();

        httpContext?.Response.Headers.SetCookie.FirstOrDefault()?.Contains(Constants.BearerToken).Should().BeTrue();
    }

    [Theory]
    [InlineData("henry.cavil@gmail.com", "henry.cavil")]
    [InlineData("test.user@outlook.com", "test.user")]
    public async Task When_User_Is_Not_Found_Expect_Unauthorized_Response(string email, string password)
    {
        // Arrange
        PostLoginHandler sut = SetupMockOptions()
                              .SetupMockHttpContext()
                              .SetupMockFindByEmail(false)
                              .Build();

        PostLoginRequest request = CreateRequest(email, password);

        // Act
        PostLoginResponse response = await sut.Handle(request, CancellationToken.None);

        // Assert
        response.IsSuccessful.Should().BeFalse();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        response.Message.Should().Be(Messages.UnauthorizedAttempt);
    }

    [Theory]
    [InlineData("henry.cavil@gmail.com", "henryCavil")]
    [InlineData("test.user@outlook.com", "testUser")]
    public async Task When_Credentials_Invalid_Expect_Unauthorized_Response(string email, string password)
    {
        // Arrange
        PostLoginHandler sut = SetupMockOptions()
                              .SetupMockHttpContext()
                              .SetupMockFindByEmail(true)
                              .SetupMockCheckUserPassword(false)
                              .Build();

        PostLoginRequest request = CreateRequest(email, password);

        // Act
        PostLoginResponse response = await sut.Handle(request, CancellationToken.None);

        // Assert
        response.IsSuccessful.Should().BeFalse();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        response.Message.Should().Be(Messages.UnauthorizedAttempt);
    }
}