using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSets.Types;
public static class EndpointsExtensions
{
    public static (LeftEndpoint<T> left, RightEndpoint<T> right) Endpoints<T>(this Interval<T> interval)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return (new LeftEndpoint<T>(interval), new RightEndpoint<T>(interval));
    }

    public static LeftEndpoint<T> LeftEndpoint<T>(this Interval<T> interval)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return new LeftEndpoint<T>(interval);
    }

    public static RightEndpoint<T> RightEndpoint<T>(this Interval<T> interval)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return new RightEndpoint<T>(interval);
    }
}
