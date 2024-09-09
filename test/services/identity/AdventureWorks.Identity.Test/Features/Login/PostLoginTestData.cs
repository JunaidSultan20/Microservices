using AdventureWorks.Identity.Application.Features.Login.Handler;
using AdventureWorks.Identity.Application.Features.Login.Request;

namespace AdventureWorks.Identity.Test.Features.Login;

public class PostLoginTestData
{
    private readonly Mock<UserManager<User>> _mockUserManager;
    protected readonly Mock<IHttpContextAccessor> MockHttpContextAccessor;
    private readonly Mock<IOptionsMonitor<JwtOptions>> _mockJwtOptions;
    private User? _user;

    protected PostLoginTestData()
    {
        Mock<IUserStore<User>> userStoreMock = new();
        _mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!,                                                        null!);
        MockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockJwtOptions = new Mock<IOptionsMonitor<JwtOptions>>();
        _user = new User(username: "tomcruise",
                         normalizedUsername: "TOMCRUISE",
                         email: "tom.cruise@gmail.com",
                         normalizedEmail: "TOM.CRUISE@GMAIL.COM",
                         emailConfirmed: true) { Id = 1 };
    }

    public PostLoginTestData SetupMockFindByEmail(bool userExists)
    {
        _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(userExists ? _user : null);
        return this;
    }

    public PostLoginTestData SetupMockCheckUserPassword(bool successful)
    {
        _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .ReturnsAsync(successful);
        return this;
    }

    public PostLoginTestData SetupMockGetRoles()
    {
        _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                        .ReturnsAsync(new List<string> { "Test" });
        return this;
    }

    public PostLoginTestData SetupMockGetAuthenticationToken(bool successful)
    {
        _mockUserManager.Setup(x => x.GetAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(successful ? "refresh-token" : null);
        return this;
    }

    public PostLoginTestData SetupMockRemoveAuthenticationToken(bool successful)
    {
        _mockUserManager.Setup(x => x.RemoveAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(successful ? IdentityResult.Success : IdentityResult.Failed());
        return this;
    }

    public PostLoginTestData SetupMockGenerateUserToken()
    {
        _mockUserManager.Setup(x => x.GenerateUserTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync("refresh-token");
        return this;
    }

    public PostLoginTestData SetupMockSetAuthenticationToken(bool successful)
    {
        _mockUserManager.Setup(x => x.SetAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), 
                                                                  It.IsAny<string>()))
                        .ReturnsAsync(successful ? IdentityResult.Success : IdentityResult.Failed());
        return this;
    }

    public PostLoginTestData SetupMockHttpContext()
    {
        var context = new DefaultHttpContext();
        
        MockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

        return this;
    }

    protected PostLoginTestData SetupMockOptions()
    {
        JwtOptions options = new JwtOptions
        {
            Audience = "https://www.xyz.com/",
            ExpirationMinutes = 5,
            Issuer = "https://www.abc.com/",
            Secret = "jwt-secret"
        };

        _mockJwtOptions.Setup(x => x.CurrentValue).Returns(options);

        return this;
    }

    protected PostLoginRequest CreateRequest(string email, string password)
    {
        return new PostLoginRequest(new AuthenticationDto(email, password));
    }

    public PostLoginHandler Build()
    {
        return new PostLoginHandler(_mockUserManager.Object, MockHttpContextAccessor.Object, _mockJwtOptions.Object);
    }
}