namespace AdventureWorks.Identity.Application.Features.Register.Handler;

public class PostRegisterHandler : IRequestHandler<PostRegisterRequest, PostRegisterResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public PostRegisterHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<PostRegisterResponse> Handle(PostRegisterRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        User? user = await _userManager.FindByEmailAsync(request.RegistrationDto.Email);

        if (user is not null)
            return new ConflictPostRegisterResponse();

        user = new User(username: request.RegistrationDto.UserName,
                        normalizedUsername: request.RegistrationDto.UserName!.ToUpper(),
                        email: request.RegistrationDto.Email,
                        normalizedEmail: request.RegistrationDto.Email.ToUpper(),
                        emailConfirmed: true);

        IdentityResult result = await _userManager.CreateAsync(user: user,
                                                               password: request.RegistrationDto.Password);

        if (result.Succeeded && request.RegistrationDto.Roles.Count > 0)
        {
            foreach (string role in request.RegistrationDto.Roles)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                    await _roleManager.CreateAsync(new Role(name: role, normalizedName: role.ToUpper()));

                result = await _userManager.AddToRoleAsync(user: user, role: role);

                if (!result.Succeeded)
                    return new BadRequestPostRegisterResponse(Messages.UnableToCreateUser);
            }

            return new PostRegisterResponse(HttpStatusCode.Created,
                                            message: Messages.UserCreatedSuccessfully,
                                            result: new UserDto(userName: user.UserName, email: user.Email));
        }

        return new BadRequestPostRegisterResponse(Messages.UserCreatedFailed);
    }
}