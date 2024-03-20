using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AdventureWorks.Common.Extensions;

public static class ArgumentCheckExtensions
{
    [return: NotNull]
    public static T IsNotNull<T>(this T t, [CallerArgumentExpression("t")] string? param = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(t, param);
        return t;
    }
}