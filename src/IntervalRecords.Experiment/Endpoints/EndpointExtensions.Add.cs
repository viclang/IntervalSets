using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Endpoints;
public static partial class EndpointExtensions
{
    public static Endpoint<T> Add<T>(this Endpoint<T> left, Endpoint<T> right)
    where T : struct, INumber<T>
    {
        return Add(left, right, (x, y) => x + y);
    }

    public static Endpoint<DateTime> Add(this Endpoint<DateTime> left, Endpoint<TimeSpan> right)
    {
        return Add(left, right, (x, y) => x + y);
    }

    internal static Endpoint<TResult> Add<TLeft, TRight, TResult>(
        Endpoint<TLeft> left,
        Endpoint<TRight> right,
        Func<TLeft, TRight, TResult> add)
    where TLeft : struct, IComparable<TLeft>, ISpanParsable<TLeft>
    where TRight : struct, IComparable<TRight>, ISpanParsable<TRight>
    where TResult : struct, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (left.IsFinite && right.IsFinite)
        {
            return new(add(left.Point.Value, right.Point.Value), left.Inclusive, left.EndpointType);
        }
        if (left.IsInfinity && right.IsInfinity && left.IsPositiveInfinity == right.IsPositiveInfinity)
        {
            return new(null, left.Inclusive, left.EndpointType);
        }
        if (left.IsInfinity && right.IsFinite)
        {
            return new(null, left.Inclusive, left.EndpointType);
        }
        if (left.IsFinite && right.IsInfinity)
        {
            return new(null, right.Inclusive, right.EndpointType);
        }
        throw new NotSupportedException("NaN");
    }
}
