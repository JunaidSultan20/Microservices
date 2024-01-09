// Global using directives

global using AdventureWorks.Common.Constants;
global using AdventureWorks.Common.Options;
global using AdventureWorks.Common.Response;
global using AdventureWorks.Identity.Application.Dto;
global using AdventureWorks.Identity.Application.Exceptions;
global using AdventureWorks.Identity.Application.Features.Login.Request;
global using AdventureWorks.Identity.Application.Features.Login.Response;
global using AdventureWorks.Identity.Application.Features.RefreshToken.Request;
global using AdventureWorks.Identity.Application.Features.RefreshToken.Response;
global using AdventureWorks.Identity.Application.Features.Register.Request;
global using AdventureWorks.Identity.Application.Features.Register.Response;
global using AdventureWorks.Identity.Domain.Entities;
global using MediatR;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using AdventureWorks.Common.Options.Setup;
global using Microsoft.Extensions.DependencyInjection;
