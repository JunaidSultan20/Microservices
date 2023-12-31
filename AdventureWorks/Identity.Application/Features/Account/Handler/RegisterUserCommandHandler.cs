﻿using System.Net;
using AdventureWorks.Common.Response;
using Identity.Application.DTOs;
using Identity.Application.Features.Account.Command;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Account.Handler;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse<UserDto>>
{
    private readonly UserManager<User?> _userManager;
    private readonly RoleManager<Role?> _roleManager;

    public RegisterUserCommandHandler(UserManager<User?> userManager, RoleManager<Role?> roleManager)
    {
        _userManager = userManager ??
                       throw new Exception("Argument null exception", new ArgumentNullException(nameof(userManager)));
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken = default)
    {
        User? user = await _userManager.FindByEmailAsync(request.RegistrationDto?.Email);
        if (user != null)
        {
            return new BaseResponse<UserDto>(HttpStatusCode.BadRequest, "User already exists.");
        }

        user = new User
        {
            UserName = request.RegistrationDto?.UserName,
            NormalizedUserName = request.RegistrationDto?.UserName?.ToUpper(),
            Email = request.RegistrationDto?.Email,
            NormalizedEmail = request.RegistrationDto?.Email?.ToUpper(),
            EmailConfirmed = true
        };
        IdentityResult result = await _userManager.CreateAsync(user, request.RegistrationDto?.Password);
        if (result.Succeeded)
        {
            if (request.RegistrationDto?.Roles is not null)
            {
                if (request.RegistrationDto.Roles is not null)
                {
                    foreach (string role in request.RegistrationDto.Roles)
                    {
                        var roleExists = await _roleManager.RoleExistsAsync(role);
                        if (!roleExists)
                            await _roleManager.CreateAsync(new Role() { Name = role, NormalizedName = role.ToUpper() });
                        result = await _userManager.AddToRoleAsync(user, role);
                        if (!result.Succeeded)
                            return new BaseResponse<UserDto>(HttpStatusCode.BadRequest, "Unable to create new user")
                                { Errors = result.Errors.Select(x => x.Description).ToList() };
                    }
                }
            }

            return new BaseResponse<UserDto>(HttpStatusCode.Created, "User Created Successfully.",
                new UserDto { UserName = user.UserName, Email = user.Email });
        }
        return new BaseResponse<UserDto>(HttpStatusCode.BadRequest, "Unable To Create New User.")
            { Errors = result.Errors.Select(x => x.Description).ToList() };
    }
}