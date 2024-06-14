using AdventureWorks.Identity.Application.Dto;
using AdventureWorks.Identity.Application.Features.Login.Handler;
using AdventureWorks.Identity.Application.Features.Login.Request;
using AdventureWorks.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AdventureWorks.Identity.Test.Features.PostLogin;

public class PostLoginTestData
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    protected readonly Mock<IHttpContextAccessor> HttpContextAccessorMock;
    private readonly Mock<IOptionsMonitor<JwtOptions>> _jwtOptionsMock;
    private User? _user;

    protected PostLoginTestData()
    {
        Mock<IUserStore<User>> userStoreMock = new();
        _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, 
                                                       null!, 
                                                       null!, 
                                                       null!, 
                                                       null!, 
                                                       null!, 
                                                       null!, 
                                                       null!, 
                                                       null!);
        HttpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _jwtOptionsMock = new Mock<IOptionsMonitor<JwtOptions>>();
        _user = new User(username: "tomcruise",
                         normalizedUsername: "TOMCRUISE",
                         email: "tom.cruise@gmail.com",
                         normalizedEmail: "TOM.CRUISE@GMAIL.COM",
                         emailConfirmed: true) { Id = 1 };
    }

    public PostLoginTestData SetupMockUserManager()
    {
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(_user);

        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .ReturnsAsync(true);

        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                        .ReturnsAsync(new List<string> { "Test" });

        _userManagerMock.Setup(x => x.GetAuthenticationTokenAsync(It.IsAny<User>(), 
                                                                  It.IsAny<string>(), 
                                                                  It.IsAny<string>()))
                        .ReturnsAsync("refresh-token");

        _userManagerMock.Setup(x => x.RemoveAuthenticationTokenAsync(It.IsAny<User>(), 
                                                                     It.IsAny<string>(), 
                                                                     It.IsAny<string>()))
                        .ReturnsAsync(new IdentityResult());

        _userManagerMock.Setup(x => x.GenerateUserTokenAsync(It.IsAny<User>(), 
                                                             It.IsAny<string>(), 
                                                             It.IsAny<string>()))
                        .ReturnsAsync("refresh-token");

        _userManagerMock.Setup(x => x.SetAuthenticationTokenAsync(It.IsAny<User>(), 
                                                                  It.IsAny<string>(), 
                                                                  It.IsAny<string>(), 
                                                                  It.IsAny<string>()))
                        .ReturnsAsync(new IdentityResult());

        return this;
    }

    public PostLoginTestData SetupMockUserManagerWhenUserIsNull()
    {
        _user = new User();

        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(_user);

        return this;
    }

    public PostLoginTestData SetupMockUserManagerWhenCredentialsInvalid()
    {
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(_user);

        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .ReturnsAsync(false);

        return this;
    }

    public PostLoginTestData SetupMockHttpContext()
    {
        var context = new DefaultHttpContext();
        
        HttpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

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

        _jwtOptionsMock.Setup(x => x.CurrentValue).Returns(options);

        return this;
    }

    protected PostLoginRequest CreateRequest(string email, string password)
    {
        return new PostLoginRequest(new AuthenticationDto(email, password));
    }

    public PostLoginHandler Build()
    {
        return new PostLoginHandler(_userManagerMock.Object, HttpContextAccessorMock.Object, _jwtOptionsMock.Object);
    }
}