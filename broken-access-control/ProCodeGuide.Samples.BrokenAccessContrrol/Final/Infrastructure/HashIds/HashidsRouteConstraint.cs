using System.Globalization;
using HashidsNet;

namespace ProCodeGuide.Samples.BrokenAccessControl.Infrastructure.HashIds;

public class HashidsRouteConstraint : IRouteConstraint
{
    private readonly IHashids hashids;

    public HashidsRouteConstraint(IHashids hashids)
    {
        this.hashids = hashids ?? throw new ArgumentNullException(nameof(hashids));
    }

    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value))
        {
            var hashid = Convert.ToString(value, CultureInfo.InvariantCulture);
            var decode = hashids.Decode(hashid);

            return decode.Length > 0;
        }

        return false;
    }
}