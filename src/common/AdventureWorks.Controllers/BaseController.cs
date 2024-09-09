using AdventureWorks.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Controllers;

/// <summary>
/// Base controller class for API controllers, providing access to common services such as IMediator, IHttpContextAccessor, and ILogger.
/// </summary>
/// <typeparam name="TController">The type of the controller inheriting from this base class.</typeparam>
[Route("api/[controller]")]
[ApiController]
[Produces(contentType: Constants.ContentTypeJson, additionalContentTypes: Constants.ContentTypeJsonHateoas)]
public abstract class BaseController<TController> : ControllerBase where TController : BaseController<TController>
{
    /// <summary>
    /// Gets the <see cref="IMediator"/> instance used for sending commands and queries.
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// Gets the <see cref="IHttpContextAccessor"/> instance, which provides access to the current HTTP context. This may be null.
    /// </summary>
    protected readonly IHttpContextAccessor? HttpContextAccessor;

    /// <summary>
    /// Gets the <see cref="ILogger{T}"/> instance of type <typeparamref name="TController"/> for logging purposes.
    /// </summary>
    protected readonly ILogger<TController> Logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseController{TController}"/> class using the provided service provider to resolve dependencies.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve dependencies.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <see cref="IMediator"/>, <see cref="ILogger{T}"/>, or <see cref="IHttpContextAccessor"/> services cannot be resolved.</exception>

    protected BaseController(IServiceProvider serviceProvider)
    {
        Mediator = serviceProvider.GetRequiredService<IMediator>() ?? throw new ArgumentNullException(nameof(Mediator));
        HttpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>() ?? throw new ArgumentNullException(nameof(HttpContextAccessor));
        Logger = serviceProvider.GetRequiredService<ILogger<TController>>() ?? throw new ArgumentNullException(nameof(Logger));
    }

    /// <summary>
    /// Gets the remote client IP address if available in the request headers.
    /// </summary>
    /// <returns>
    /// The client IP address as a string if available, otherwise null.
    /// </returns>
    protected string? RemoteIpAddress
    {
        get
        {
            if (HttpContextAccessor?.HttpContext?.Request?.Headers.TryGetValue(Constants.ForwardedFor, out var header) == true)
            {
                return header;
            }
            return null;
        }
    }
}