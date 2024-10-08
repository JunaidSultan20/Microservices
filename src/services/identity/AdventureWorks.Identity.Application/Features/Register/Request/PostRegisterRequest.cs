﻿using AdventureWorks.Identity.Application.Features.Register.Response;

namespace AdventureWorks.Identity.Application.Features.Register.Request;

public class PostRegisterRequest(RegistrationDto registrationDto) : IRequest<PostRegisterResponse>
{
    public RegistrationDto RegistrationDto { get; } = registrationDto;
}