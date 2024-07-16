using Microsoft.AspNetCore.Routing;

namespace AdventureWorks.Common.Constraints;

public class StringRouteConstraint(string routeValue) : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, 
                      IRouter? route, 
                      string routeKey, 
                      RouteValueDictionary values, 
                      RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out object? value) && value != null)
        {
            if (value.ToString()!.Equals(routeValue, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}