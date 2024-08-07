using AdventureWorks.Identity.Application.Features.Register.Handler;
using AdventureWorks.Identity.Application.Features.Register.Request;
using AdventureWorks.Identity.Application.Features.Register.Response;

namespace AdventureWorks.Identity.Test.Features.Register.Handler;

public class PostRegisterTest : PostRegisterTestData
{
    [Theory]
    [InlineData("test.user.1", "test.user.1@outlook.com", "testUser@@1", "User")]
    [InlineData("test.user.2", "test.user.2@outlook.com", "testUser@@2", "User")]
    [InlineData("test.user.3", "test.user.3@outlook.com", "testUser@@3", "User")]
    [InlineData("test.user.4", "test.user.4@outlook.com", "testUser@@4", "User")]
    [InlineData("test.user.5", "test.user.5@outlook.com", "testUser@@5", "User")]
    public async Task When_User_Register_Is_Passed_Expect_Registration_Response(string username, string email, string password, string role)
    {
        // Arrange
        PostRegisterHandler sut = SetupMockFindByEmail(false)
                                 .SetupMockCreateUser(true)
                                 .SetupMockRoleExists(true)
                                 .SetupMockAddToRole(true)
                                 .SetupMockFindRoleByName(role)
                                 .SetupMockEventStoreForUserAggregate()
                                 .Build();

        PostRegisterRequest request = CreateRequest(username, email, password, role);

        // Act
        PostRegisterResponse result = await sut.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PostRegisterResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.IsSuccessful.Should().BeTrue();
        result.Message.Should().Be(Messages.UserCreatedSuccessfully);
        result.Result.Should().BeOfType<UserDto>();
        result.Result.Should().NotBeNull();
        result.Result?.UserName.Should().Be(username);
        result.Result?.Email.Should().Be(email);
    }

    [Theory]
    [InlineData("test.user.1", "test.user.1@outlook.com", "testUser@@1", "User")]
    [InlineData("test.user.2", "test.user.2@outlook.com", "testUser@@2", "User")]
    [InlineData("test.user.3", "test.user.3@outlook.com", "testUser@@3", "User")]
    [InlineData("test.user.4", "test.user.4@outlook.com", "testUser@@4", "User")]
    [InlineData("test.user.5", "test.user.5@outlook.com", "testUser@@5", "User")]
    public async Task When_User_Exists_Expect_Conflict_Response(string username, string email, string password, string role)
    {
        // Arrange
        PostRegisterHandler sut = SetupMockFindByEmail(true)
                                 .Build();

        PostRegisterRequest request = CreateRequest(username, email, password, role);

        // Act
        PostRegisterResponse result = await sut.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ConflictPostRegisterResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.Conflict);
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be(Messages.UserExists);
        result.Result.Should().BeNull();
    }

    [Theory]
    [InlineData("test.user.1", "test.user.1@outlook.com", "testUser@@1", "User")]
    [InlineData("test.user.2", "test.user.2@outlook.com", "testUser@@2", "User")]
    [InlineData("test.user.3", "test.user.3@outlook.com", "testUser@@3", "User")]
    [InlineData("test.user.4", "test.user.4@outlook.com", "testUser@@4", "User")]
    [InlineData("test.user.5", "test.user.5@outlook.com", "testUser@@5", "User")]
    public async Task When_Unable_To_Create_User_Expect_Bad_Request_Response(string username, string email, string password, string role)
    {
        // Arrange
        PostRegisterHandler sut = SetupMockFindByEmail(false)
                                 .SetupMockCreateUser(false)
                                 .Build();

        PostRegisterRequest request = CreateRequest(username, email, password, role);

        // Act
        PostRegisterResponse result = await sut.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestPostRegisterResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be(Messages.UnableToCreateUser);
        result.Result.Should().BeNull();
    }

    [Theory]
    [InlineData("test.user.1", "test.user.1@outlook.com", "testUser@@1", "User")]
    [InlineData("test.user.2", "test.user.2@outlook.com", "testUser@@2", "User")]
    [InlineData("test.user.3", "test.user.3@outlook.com", "testUser@@3", "User")]
    [InlineData("test.user.4", "test.user.4@outlook.com", "testUser@@4", "User")]
    [InlineData("test.user.5", "test.user.5@outlook.com", "testUser@@5", "User")]
    public async Task When_Role_Does_Not_Exists_Expect_Not_Fount_Response(string username, string email, string password, string role)
    {
        // Arrange
        PostRegisterHandler sut = SetupMockFindByEmail(false)
                                 .SetupMockCreateUser(true)
                                 .SetupMockRoleExists(false)
                                 .Build();

        PostRegisterRequest request = CreateRequest(username, email, password, role);

        // Act
        PostRegisterResponse result = await sut.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundPostRegisterResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be(Messages.RoleNotFound);
        result.Result.Should().BeNull();
    }

    [Theory]
    [InlineData("test.user.1", "test.user.1@outlook.com", "testUser@@1", "User")]
    [InlineData("test.user.2", "test.user.2@outlook.com", "testUser@@2", "User")]
    [InlineData("test.user.3", "test.user.3@outlook.com", "testUser@@3", "User")]
    [InlineData("test.user.4", "test.user.4@outlook.com", "testUser@@4", "User")]
    [InlineData("test.user.5", "test.user.5@outlook.com", "testUser@@5", "User")]
    public async Task When_Unable_To_Assign_Role_Expect_Bad_Request_Response(string username, string email, string password, string role)
    {
        // Arrange
        PostRegisterHandler sut = SetupMockFindByEmail(false)
                                 .SetupMockCreateUser(true)
                                 .SetupMockRoleExists(true)
                                 .SetupMockAddToRole(false)
                                 .Build();

        PostRegisterRequest request = CreateRequest(username, email, password, role);

        // Act
        PostRegisterResponse result = await sut.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestPostRegisterResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccessful.Should().BeFalse();
        result.Message.Should().Be(Messages.UnableToAssignRole);
        result.Result.Should().BeNull();
    }
}