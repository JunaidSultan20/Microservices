using AdventureWorks.Contracts.EventStreaming;
using AdventureWorks.Identity.Application.DomainEvents;
using AdventureWorks.Identity.Application.Features.Register.Handler;
using AdventureWorks.Identity.Application.Features.Register.Request;

namespace AdventureWorks.Identity.Test.Features.Register;

public class PostRegisterTestData
{
    private readonly Mock<UserManager<User>> _mockUserManager;
    private readonly Mock<RoleManager<Role>> _mockRoleManager;
    private readonly Mock<UserAggregate> _mockUserAggregate;
    private readonly Mock<IEventStore> _mockEventStore;
    private User? _user;

    protected PostRegisterTestData()
    {
        Mock<IUserStore<User>> userStoreMock = new();
        Mock<IRoleStore<Role>> roleStoreMock = new();
        _mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        _mockRoleManager = new Mock<RoleManager<Role>>(roleStoreMock.Object, null!, null!, null!, null!);
        _mockUserAggregate = new Mock<UserAggregate>();
        _mockEventStore = new Mock<IEventStore>();
        _user = new User(username: "tomcruise",
                         normalizedUsername: "TOMCRUISE",
                         email: "tom.cruise@gmail.com",
                         normalizedEmail: "TOM.CRUISE@GMAIL.COM",
                         emailConfirmed: true) { Id = 1 };
    }

    public PostRegisterTestData SetupMockFindByEmail(bool userExists)
    {
        _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(userExists ? _user : null);
        return this;
    }

    public PostRegisterTestData SetupMockCreateUser(bool successful)
    {
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .ReturnsAsync(successful ? IdentityResult.Success : IdentityResult.Failed());
        return this;
    }

    public PostRegisterTestData SetupMockAddToRole(bool successful)
    {
        _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .ReturnsAsync(successful ? IdentityResult.Success : IdentityResult.Failed());
        return this;
    }

    public PostRegisterTestData SetupMockRoleExists(bool successful)
    {
        _mockRoleManager.Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                        .ReturnsAsync(successful);
        return this;
    }

    public PostRegisterTestData SetupMockFindRoleByName(string? roleName)
    {
        _mockRoleManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                        .ReturnsAsync(!string.IsNullOrEmpty(roleName) ? new Role(name: "Test-Role", normalizedName: "TEST-ROLE") { Id = 1 } : null);
        return this;
    }

    public PostRegisterTestData SetupMockEventStoreForUserAggregate()
    {
        _mockEventStore.Setup(x => x.SaveAsync(It.IsAny<UserAggregate>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.CompletedTask);
        return this;
    }

    protected PostRegisterRequest CreateRequest(string username, string email, string password, string role)
    {
        return new PostRegisterRequest(new RegistrationDto(username, email, password, role));
    }

    public PostRegisterHandler Build()
    {
        return new PostRegisterHandler(_mockUserManager.Object, 
                                       _mockRoleManager.Object, 
                                       _mockUserAggregate.Object, 
                                       _mockEventStore.Object);
    }
}