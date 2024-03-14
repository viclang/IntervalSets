using IntervalRecords.Experiment.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Extensions.Calculate;
public static partial class IntervalExtensions
{
    public static T Minimum<T, L, R>(this Interval<T, L, R> interval)
        where T : struct, IComparable<T>, ISpanParsable<T>, IMinMaxValue<T>
        where L : IBound, new()
        where R : IBound, new()
    {
        return L.Bound is Bound.Unbounded ? T.MinValue : interval.Start.GetValueOrDefault();
    }

    public static T Maximum<T, L, R>(this Interval<T, L, R> interval)
        where T : struct, IComparable<T>, ISpanParsable<T>, IMinMaxValue<T>
        where L : IBound, new()
        where R : IBound, new()
    {
        return R.Bound is Bound.Unbounded ? T.MaxValue : interval.End.GetValueOrDefault();
    }
}
