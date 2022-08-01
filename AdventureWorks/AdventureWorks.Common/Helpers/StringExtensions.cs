using Microsoft.Extensions.Primitives;

namespace AdventureWorks.Common.Helpers;

public static class StringExtensions
{
    public static bool NotNullAndEquals(this string? value, string valueToMatch)
    {
        if (value is not null && value.Equals(valueToMatch))
            return true;
        return false;
    }

    public static bool NotNullAndEquals(this StringSegment? value, string valueToMatch)
    {
        if (value is not null && value.Equals(valueToMatch))
            return true;
        return false;
    }
}