namespace AdventureWorks.Identity.Application.Exceptions;

public class RefreshTokenException() : Exception(message: Messages.RefreshTokenNotFound);