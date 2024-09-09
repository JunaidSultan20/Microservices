using AdventureWorks.Contracts.EventStreaming;
using AdventureWorks.Events.Streams;
using AdventureWorks.Identity.Application.DomainEvents;
using AdventureWorks.Identity.Application.Features.Register.Request;
using AdventureWorks.Identity.Application.Features.Register.Response;

namespace AdventureWorks.Identity.Application.Features.Register.Handler;

public class PostRegisterHandler(UserManager<User> userManager, 
                                 RoleManager<Role> roleManager,
                                 UserAggregate userAggregate,
                                 IEventStore eventStore) : IRequestHandler<PostRegisterRequest, PostRegisterResponse>
{
    public async Task<PostRegisterResponse> Handle(PostRegisterRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        User? user = await userManager.FindByEmailAsync(request.RegistrationDto.Email);

        if (user is not null)
            return new ConflictPostRegisterResponse();

        user = new User(username: request.RegistrationDto.Username,
                        normalizedUsername: request.RegistrationDto.Username!.ToUpper(),
                        email: request.RegistrationDto.Email,
                        normalizedEmail: request.RegistrationDto.Email.ToUpper(),
                        emailConfirmed: true);

        IdentityResult result = await userManager.CreateAsync(user: user, password: request.RegistrationDto.Password);

        if (!result.Succeeded)
            return new BadRequestPostRegisterResponse(Messages.UnableToCreateUser);

        bool roleExists = await roleManager.RoleExistsAsync(request.RegistrationDto.Role);

        if (!roleExists)
            return new NotFoundPostRegisterResponse(Messages.RoleNotFound);

        result = await userManager.AddToRoleAsync(user: user, role: request.RegistrationDto.Role);

        if (!result.Succeeded)
            return new BadRequestPostRegisterResponse(Messages.UnableToAssignRole);

        Role? role = await roleManager.FindByNameAsync(request.RegistrationDto.Role);

        userAggregate.UserCreatedEvent(username: user.UserName ?? string.Empty, 
                                       email: user.Email ?? string.Empty, 
                                       password: user.PasswordHash ?? string.Empty, 
                                       role: role?.Name ?? string.Empty);

        if (role is not null)
        {
            userAggregate.UserRoleChangedEvent(null, role.Id);

            await eventStore.SaveAsync(userAggregate, user.Id.ToString(), IdentityStreams.UserStream);
        }

        return new PostRegisterResponse(statusCode: HttpStatusCode.Created, 
                                        message: Messages.UserCreatedSuccessfully, 
                                        result: new UserDto(userName: user.UserName, email: user.Email));
    }
}