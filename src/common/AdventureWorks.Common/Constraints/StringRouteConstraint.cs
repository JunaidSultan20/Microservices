using Microsoft.AspNetCore.Routing;

namespace AdventureWorks.Common.Constraints;

/// <summary>
/// Defines a constraint that matches a route value against a specified string.
/// </summary>
public class StringRouteConstraint(string routeValue) : IRouteConstraint
{
    /// <summary>
    /// Determines whether the route value for the given route key matches the specified string value.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="route">The router associated with this constraint.</param>
    /// <param name="routeKey">The key of the route value to check.</param>
    /// <param name="values">The route values.</param>
    /// <param name="routeDirection">The route direction, indicating whether it's incoming or outgoing.</param>
    /// <returns>
    /// <c>true</c> if the route value matches the specified string value; otherwise, <c>false</c>.
    /// </returns>
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out object? value) && value != null)
        {
            if (value.ToString()!.Equals(routeValue, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}