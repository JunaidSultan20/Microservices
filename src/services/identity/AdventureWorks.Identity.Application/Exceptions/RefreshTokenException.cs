namespace AdventureWorks.Identity.Application.Exceptions;

public class RefreshTokenException : Exception
{
    public RefreshTokenException() : base(message: Messages.RefreshTokenNotFound)
    {
    }
}