namespace AdventureWorks.Identity.Application.Exceptions;

/// <summary>
/// The exception class that is thrown when refreshing the access token.
/// </summary>
public class RefreshTokenException() : Exception(message: Messages.RefreshTokenNotFound);