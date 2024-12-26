using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for performing argument checks.
/// </summary>
public static class ArgumentCheckExtensions
{
    /// <summary>
    /// Ensures that the specified argument is not null. Throws an <see cref="ArgumentNullException"/> 
    /// if the argument is null, using the provided parameter name for the exception message.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    /// <param name="t">The argument to check for null.</param>
    /// <param name="param">The name of the argument parameter (automatically populated by the caller).</param>
    /// <returns>The original argument if it is not null.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the argument is null.</exception>
    [return: NotNull]
    public static T IsNotNull<T>(this T t, [CallerArgumentExpression("t")] string? param = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(t, param);
        return t;
    }
}