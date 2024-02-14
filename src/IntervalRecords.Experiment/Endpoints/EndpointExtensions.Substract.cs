using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Endpoints;
public static partial class EndpointExtensions
{
    public static Endpoint<T> Substract<T>(
        this Endpoint<T> left,
        Endpoint<T> right)
    where T : struct, INumber<T>
    {
        return Substract(left, right, (x, y) => x + y);
    }

    public static Endpoint<DateTime> Substract(this Endpoint<DateTime> left, Endpoint<TimeSpan> right)
    {
        return Add(left, right, (x, y) => x - y);
    }
    
    public static Endpoint<TimeSpan> Substract(this Endpoint<DateTime> left, Endpoint<DateTime> right)
    {
        return Add(left, right, (x, y) => x - y);
    }

    internal static Endpoint<TResult> Substract<TSelf, TOther, TResult>(
        Endpoint<TSelf> left,
        Endpoint<TOther> right,
        Func<TSelf, TOther, TResult> substract)
    where TSelf : struct, IComparable<TSelf>, ISpanParsable<TSelf>
    where TOther : struct, IComparable<TOther>, ISpanParsable<TOther>
    where TResult : struct, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (left.IsFinite && right.IsFinite)
        {
            return new(substract(left.Point.Value, right.Point.Value));
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
