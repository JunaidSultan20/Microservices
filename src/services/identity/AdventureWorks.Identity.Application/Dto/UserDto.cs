﻿namespace AdventureWorks.Identity.Application.Dto;

public class UserDto
{
    public string? UserName { get; }
    public string? Email { get; }

    public UserDto()
    {
    }

    public UserDto(string? userName, string? email) => (UserName, Email) = (userName, email);
}